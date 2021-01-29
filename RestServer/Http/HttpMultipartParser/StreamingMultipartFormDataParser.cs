using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpMultipartParser
{
   
    public class StreamingMultipartFormDataParser : IStreamingMultipartFormDataParser
    {

        #region Fields

        private readonly string[] binaryMimeTypes = { "application/octet-stream" };

        private readonly byte[] stream;

        private string boundary;

        private byte[] boundaryBinary;

        private string endBoundary;

        private byte[] endBoundaryBinary;

        #endregion

        #region Constructors and Destructors

        public StreamingMultipartFormDataParser(byte[] stream, Encoding encoding, string[] binaryMimeTypes = null)
            : this(stream, null, encoding, binaryMimeTypes)
        {
        }

        public StreamingMultipartFormDataParser(byte[] stream, string boundary = null, Encoding encoding = null, string[] binaryMimeTypes = null)
        {
            if (stream == null) { throw new ArgumentNullException(nameof(stream)); }

            this.stream = stream;
            this.boundary = boundary;
            Encoding = encoding ?? Encoding.UTF8;
            if (binaryMimeTypes != null)
            {
                this.binaryMimeTypes = binaryMimeTypes;
            }
        }

        #endregion

        #region Public Methods

        public void Run()
        {

            if (boundary == null)
            {
                boundary = DetectBoundary(stream);
            }

            boundary = "--" + boundary;
            endBoundary = boundary + "--";

            boundaryBinary = Encoding.GetBytes(boundary);
            endBoundaryBinary = Encoding.GetBytes(endBoundary);

            Parse(stream);
        }


        #endregion

        #region Public Properties

        public int BinaryBufferSize { get; private set; }

        public Encoding Encoding { get; private set; }

        public FileStreamDelegate FileHandler { get; set; }

        public ParameterDelegate ParameterHandler { get; set; }

        public StreamClosedDelegate StreamClosedHandler { get; set; }

        #endregion

        #region Private Methods

        private static string DetectBoundary(byte[] bytes)
        {
            // Presumably the boundary is --|||||||||||||| where -- is the stuff added on to
            // the front as per the protocol and ||||||||||||| is the part we care about.
            StreamReader reader = new StreamReader(new MemoryStream(bytes));
            string boundary = string.Concat(reader.ReadLine().Skip(2));

            return boundary;
        }

        private bool IsFilePart(IDictionary<string, string> parameters)
        {
            // If a section contains filename, then it's a file.
            if (parameters.ContainsKey("filename")) return true;

            // Check if mimetype is a binary file
            else if (parameters.ContainsKey("content-type") &&
                     binaryMimeTypes.Contains(parameters["content-type"])) return true;

            // If the section is missing the filename and the name, then it's a file.
            // For example, images in an mjpeg stream have neither a name nor a filename.
            else if (!parameters.ContainsKey("name")) return true;

            // In all other cases, we assume it's a "data" parameter.
            return false;
        }

        private void Parse(byte[] bytes)
        {
            List<byte[]> parts = ByteArrayUtils.Split(bytes, boundaryBinary);
            parts.ForEach(x =>
            {
                ParseSection(x);
            });
        }

        private void ParseParameterPart(Dictionary<string, string> parameters, BytesReader reader)
        {
            var data = new StringBuilder();
            bool firstTime = true;
            string line = reader.ReadLine();
            while (line != null)
            {
                if (firstTime)
                {
                    data.Append(line);
                    firstTime = false;
                }
                else
                {
                    data.Append(Environment.NewLine);
                    data.Append(line);
                }

                line = reader.ReadLine();
            }

            var part = new ParameterPart(parameters["name"], data.ToString());
            ParameterHandler(part);
        }

        private void ParseSection(byte[] bytes)
        {

            var parameters = new Dictionary<string, string>();

            BytesReader reader = new BytesReader(bytes);

            string line = reader.ReadLine();
            while (line != string.Empty)
            {
                if (line == null)
                {
                    throw new MultipartParseException("Unexpected end of stream");
                }

                if (line == boundary || line == endBoundary)
                {
                    throw new MultipartParseException("Unexpected end of section");
                }

                // This line parses the header values into a set of key/value pairs.
                // For example:
                //   Content-Disposition: form-data; name="textdata"
                //     ["content-disposition"] = "form-data"
                //     ["name"] = "textdata"
                //   Content-Disposition: form-data; name="file"; filename="data.txt"
                //     ["content-disposition"] = "form-data"
                //     ["name"] = "file"
                //     ["filename"] = "data.txt"
                //   Content-Type: text/plain
                //     ["content-type"] = "text/plain"
                Dictionary<string, string> values = SplitBySemicolonIgnoringSemicolonsInQuotes(line)
                    .Select(x => x.Split(new[] { ':', '=' }, 2))

                    // select where the length of the array is equal to two, that way if it is only one it will
                    // be ignored as it is invalid key-pair
                    .Where(x => x.Length == 2)

                    // Limit split to 2 splits so we don't accidently split characters in file paths.
                    .ToDictionary(
                        x => x[0].Trim().Replace("\"", string.Empty).ToLower(),
                        x => x[1].Trim().Replace("\"", string.Empty));

                // Here we just want to push all the values that we just retrieved into the
                // parameters dictionary.
                try
                {
                    foreach (var pair in values)
                    {
                        parameters.Add(pair.Key, pair.Value);
                    }
                }
                catch (ArgumentException)
                {
                    throw new MultipartParseException("Duplicate field in section");
                }

                line = reader.ReadLine();
            }

            // Now that we've consumed all the parameters we're up to the body. We're going to do
            // different things depending on if we're parsing a, relatively small, form value or a
            // potentially large file.
            if (IsFilePart(parameters))
            {
                // Read the parameters
                parameters.TryGetValue("name", out string name);
                parameters.TryGetValue("filename", out string filename);
                parameters.TryGetValue("content-type", out string contentType);
                parameters.TryGetValue("content-disposition", out string contentDisposition);

                // Filter out the "well known" parameters.
                var additionalParameters = GetAdditionalParameters(parameters);

                //reader.re
                byte[]  data = reader.ReadToEnd();
               
                FileHandler(name, filename, contentType, contentDisposition, data, additionalParameters);

            }
            else
            {
                ParseParameterPart(parameters, reader);
            }
        }

        private IEnumerable<string> SplitBySemicolonIgnoringSemicolonsInQuotes(string line)
        {
            // Loop over the line looking for a semicolon. Keep track of if we're currently inside quotes
            // and if we are don't treat a semicolon as a splitting character.
            bool inQuotes = false;
            string workingString = string.Empty;

            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }

                if (c == ';' && !inQuotes)
                {
                    yield return workingString;
                    workingString = string.Empty;
                }
                else
                {
                    workingString += c;
                }
            }

            yield return workingString;
        }

        private IDictionary<string, string> GetAdditionalParameters(IDictionary<string, string> parameters)
        {
            var wellKnownParameters = new[] { "name", "filename", "content-type", "content-disposition" };
            var additionalParameters = parameters
                .Where(param => !wellKnownParameters.Contains(param.Key))
                .ToDictionary(x => x.Key, x => x.Value);
            return additionalParameters;
        }

        #endregion
    }
}