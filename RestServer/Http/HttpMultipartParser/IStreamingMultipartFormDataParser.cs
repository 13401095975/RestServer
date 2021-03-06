using System.Collections.Generic;

namespace HttpMultipartParser
{

    public delegate void FileStreamDelegate(string name, string fileName, string contentType, string contentDisposition, byte[] buffer, IDictionary<string, string> additionalProperties);

    public delegate void ParameterDelegate(ParameterPart part);


    public interface IStreamingMultipartFormDataParser
    {

        FileStreamDelegate FileHandler { get; set; }

        ParameterDelegate ParameterHandler { get; set; }


        void Run();

    }
}