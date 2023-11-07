using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EntityModels;

/// <summary>
/// Represents validation for <see cref="MarcField"/>s.
/// </summary>
[Table("validation")]
public partial class Validation
{
    /// <summary>
    /// Gets or sets the identifier for the validation.
    /// </summary>
    [Key]
    [Column("id_validation")]
    public long IdValidation { get; set; }

    /// <summary>
    /// Gets or sets the validation with a maximum length of 500 characters.
    /// </summary>
    [Column("validation", TypeName = "VARCHAR(500)")]
    public string Validation1 { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of <see cref="FieldValidation"/> entities associated with this validation.
    /// </summary>
    [InverseProperty("IdValidationNavigation")]
    public virtual ICollection<FieldValidation> FieldValidations { get; set; } = new List<FieldValidation>();
}

