using System.Text.Json.Serialization;

namespace ComunicationDataLayer.POCOs
{
    public class ValidationSet
    {
        public string Name { get; set; }
        public List<ValidationBase> ValidationList { get; set; }
    }

    public abstract class ValidationBase
    {
        public string? Pattern { get; set; }
        public string? PatternErrorMessage { get; set; }
        public FieldObligationScope Obligation { get; set; }
        public List<ValidationBase>? Conditions { get; set; }
        public List<ValidationBase>? Alternatives { get; set; }
    }

    public class LeaderValidation : ValidationBase
    {
        public Leader Leader { get; set; }
    }

    public class ControlFieldValidation : ValidationBase
    {
        public ControlField ControlField { get; set; }
    }

    public class DataFieldValidation : ValidationBase
    {
        public DataField DataField { get; set; }
    }

    public class SubFieldValidation : ValidationBase
    {
        public SubField SubField { get; set; }
    }

    public enum FieldObligationScope
    {
        M = 1,
        MA = 2,
        R = 3,
        RA = 4,
        O = 5,
        FORBIDDEN = 6
    }

    public class Leader
    {
        public int? Index;
        public int? IndexEnd;
    }
    public class ControlField
    {
        public int Tag;
        public int? Index;
        public int? IndexEnd;
    }
    public class DataField
    {
        public int Tag;
        public string? Identificator1;
        public string? Identificator2;
    }
    public class SubField
    {
        public DataField Parrent { get; set; }
        public string Code;
    }
}
