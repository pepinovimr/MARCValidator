using System.Text.Json.Serialization;

namespace ComunicationDataLayer.POCOs
{
    /// <summary>
    /// Root of Json file with validation
    /// </summary>
    public class ValidationSet
    {
        public string Name { get; set; }
        /// <summary>
        /// List of validations to be performed
        /// </summary>
        public List<ValidationBase> ValidationList { get; set; }
    }
    /// <summary>
    /// Wrapper for one Validation
    /// <para>Other Validations inherit from this one</para>
    /// </summary>
    public abstract class ValidationBase
    {
        /// <summary>
        /// Used when value of validated field should match some regular expression
        /// </summary>
        public string? Pattern { get; set; }
        /// <summary>
        /// Message to be shown when <see cref="Pattern"/> does not match to the value of field
        /// </summary>
        public string? PatternErrorMessage { get; set; }
        /// <summary>
        /// Indicates how big of a problem when said field is not valid
        /// </summary>
        public FieldObligationScope Obligation { get; set; }
        /// <summary>
        /// Max allowed count of occurrences of said field
        /// </summary>
        public int? MaxCount { get; set; }
        /// <summary>
        /// List of validation for when field is valid, but other, dependand fields, also have to be valid
        /// </summary>
        public List<ValidationBase>? Conditions { get; set; }
        /// <summary>
        /// List of validations for when field is not valid, but some other fields can be used instead
        /// </summary>
        public List<ValidationBase>? Alternatives { get; set; }
        /// <summary>
        /// List with results for this validation. Is not (de)serailized
        /// </summary>
        [JsonIgnore]
        public List<Result> ValidationResults { get; set; } = [];
    }

    /// <summary>
    /// Validation for Leader type
    /// </summary>
    public class LeaderValidation : ValidationBase
    {
        /// <summary>
        /// Leader of file
        /// </summary>
        public Leader Leader { get; set; }
        /// <summary>
        /// There can be just 1 Leader in MARC record
        /// </summary>
        private new readonly int MaxCount = 1;
    }

    /// <summary>
    /// Validation for ControlFields
    /// </summary>
    public class ControlFieldValidation : ValidationBase
    {
        /// <summary>
        /// COntrol field to validate
        /// </summary>
        public ControlField ControlField { get; set; }
        /// <summary>
        /// There can be just 1 ControlField for each tag in MARC record
        /// </summary>
        private new readonly int MaxCount = 1;
    }

    /// <summary>
    /// Validation for DataField
    /// <para>Only to be used for validation of <see cref="FieldObligationScope"/>, its value is validated id <see cref="SubFieldValidation"/></para>
    /// </summary>
    public class DataFieldValidation : ValidationBase
    {
        /// <summary>
        /// Datafield to validate
        /// </summary>
        public DataField DataField { get; set; }
    }

    /// <summary>
    /// Validation for Datafield
    /// </summary>
    public class SubFieldValidation : ValidationBase
    {
        /// <summary>
        /// Subfield to validate
        /// </summary>
        public SubField SubField { get; set; }
    }

    /// <summary>
    /// Obligation of Validation
    /// </summary>
    public enum FieldObligationScope
    {
        /// <summary>
        /// Field should always exist
        /// </summary>
        M = 1,
        /// <summary>
        /// Field should always exist when its value is avaiable
        /// </summary>
        MA = 2,
        /// <summary>
        /// Field is reccomended
        /// </summary>
        R = 3,
        /// <summary>
        /// Field is recomended when its value is avaiable
        /// </summary>
        RA = 4,
        /// <summary>
        /// Field is optional
        /// </summary>
        O = 5,
        /// <summary>
        /// Field should not exist
        /// </summary>
        FORBIDDEN = 6
    }

    /// <summary>
    /// Specifies range of Leader indexes
    /// </summary>
    public class Leader
    {
        /// <summary>
        /// [optional] First, or only index
        /// </summary>
        public int? Index;
        /// <summary>
        /// [optional] end of index range
        /// </summary>
        public int? IndexEnd;
    }

    /// <summary>
    /// Specifies ControlField and optionaly its indexes
    /// </summary>
    public class ControlField
    {
        /// <summary>
        /// Tag of ControlField
        /// </summary>
        public int Tag;
        /// <summary>
        /// [optional] First, or only index
        /// </summary>
        public int? Index;
        /// <summary>
        /// [optional] end of index range
        /// </summary>
        public int? IndexEnd;
    }
    /// <summary>
    /// Specifies DataField and optionaly its identificators
    /// </summary>
    public class DataField
    {
        /// <summary>
        /// DataField tag
        /// </summary>
        public int Tag;
        /// <summary>
        /// [optional] First identificator
        /// </summary>
        public string? Indicator1;
        /// <summary>
        /// [optional] Second identificator
        /// </summary>
        public string? Indicator2;
    }

    /// <summary>
    /// Specifies subfield, its code and its parent datafield
    /// </summary>
    public class SubField
    {
        /// <summary>
        /// Datafield parent of Subfield to validate
        /// </summary>
        public DataField Parent { get; set; }
        /// <summary>
        /// Code of Subfield
        /// </summary>
        public string Code;
    }
}
