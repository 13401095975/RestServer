
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HttpMultipartParser
{

    public class FilePart
    {
        #region Fields

        private const string DefaultContentType = "text/plain";
        private const string DontentDisposition = "form-data";

        #endregion

        #region Constructors and Destructors

      
        public FilePart(string name, string fileName, byte[] data, string contentType = DefaultContentType, string contentDisposition = DontentDisposition)
            : this(name, fileName, data, null, contentType, contentDisposition)
        {
        }

        public FilePart(string name, string fileName, byte[] data, IDictionary<string, string> additionalProperties, string contentType = DefaultContentType, string contentDisposition = DontentDisposition)
        {
            Name = name;
            FileName = fileName?.Split(Path.GetInvalidFileNameChars()).Last();
            Data = data;
            ContentType = contentType;
            ContentDisposition = contentDisposition;
            AdditionalProperties = new Dictionary<string, string>(additionalProperties ?? new Dictionary<string, string>());
        }

        #endregion

        #region Public Properties


        public byte[] Data { get; }

        public string FileName { get; }

        public string Name { get; }

        public string ContentType { get; }

        public string ContentDisposition { get; }

        public Dictionary<string, string> AdditionalProperties { get; private set; }

        #endregion
    }
}