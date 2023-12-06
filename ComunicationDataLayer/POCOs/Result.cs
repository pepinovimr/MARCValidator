using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
    public record Result(ResultType type, string source, string text)
    {
        public ResultType Type = type;
        public string Source = source;
        public string Text = text;
    }
}
