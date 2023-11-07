using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EntityModels;

/// <summary>
/// Represents obligation of each validation.
/// </summary>
[Table("validation_obligation")]
public partial class ValidationObligation
{
    /// <summary>
    /// Gets or sets the identifier for the validation obligation.
    /// </summary>
    [Key]
    [Column("id_validation_obligation")]
    public long IdValidationObligation { get; set; }

    /// <summary>
    /// Gets or sets the description of the validation obligation with a maximum length of 30 characters.
    /// </summary>
    [Column("obligation", TypeName = "VARCHAR(30)")]
    public string Obligation { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of <see cref="FieldValidation"/> entities associated with this validation obligation.
    /// </summary>
    [InverseProperty("IdValidationObligationNavigation")]
    public virtual ICollection<FieldValidation> FieldValidations { get; set; } = new List<FieldValidation>();
}
