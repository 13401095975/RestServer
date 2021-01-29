
using System.Collections.Generic;
using System.Text;

namespace HttpMultipartParser
{

    public class MultipartFormDataParser : IMultipartFormDataParser
    {
        public List<FilePart> Files { get; }

        public List<ParameterPart> Parameters { get; }

        private MultipartFormDataParser()
        {
            Files = new List<FilePart>();
            Parameters = new List<ParameterPart>();
        }


        public static MultipartFormDataParser Parse(byte[] stream, Encoding encoding, string[] binaryMimeTypes = null)
        {
            return Parse(stream, null, encoding, binaryMimeTypes);
        }

        public static MultipartFormDataParser Parse(byte[] stream, string boundary = null, Encoding encoding = null, string[] binaryMimeTypes = null)
        {
            var parser = new MultipartFormDataParser();
            parser.ParseStream(stream, boundary, encoding, binaryMimeTypes);
            return parser;
        }


        private void ParseStream(byte[] stream, string boundary, Encoding encoding, string[] binaryMimeTypes)
        {
            var streamingParser = new StreamingMultipartFormDataParser(stream, boundary, encoding ?? Encoding.UTF8, binaryMimeTypes);
            streamingParser.ParameterHandler += parameterPart => Parameters.Add(parameterPart);

            streamingParser.FileHandler += (name, fileName, type, disposition, buffer, additionalProperties) =>
            {
                Files.Add(new FilePart(name, fileName, buffer, additionalProperties, type, disposition));

            };

            streamingParser.Run();

        }

    }
}