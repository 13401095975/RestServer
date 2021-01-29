using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HttpMultipartParser
{

    public interface IMultipartFormDataParser
    {

        List<FilePart> Files { get; }

        List<ParameterPart> Parameters { get; }
    }
}