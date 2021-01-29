
namespace HttpMultipartParser
{

    public class ParameterPart
    {
        public string Data { get; }

        public string Name { get; }

        public ParameterPart(string name, string data)
        {
            Name = name;
            Data = data;
        }

    }
}