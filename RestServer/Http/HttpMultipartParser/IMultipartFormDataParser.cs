using System.Collections.Generic;

namespace HttpMultipartParser
{

    public interface IMultipartFormDataParser
    {

        List<FilePart> Files { get; }

        List<ParameterPart> Parameters { get; }
    }
}