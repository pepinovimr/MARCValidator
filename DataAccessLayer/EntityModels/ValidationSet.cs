using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModels;

/// <summary>
/// Represents one validation set.
/// </summary>
[Table("validation_set")]
[Index("ValidationName", Name = "validation_name_UNIQUE", IsUnique = true)]
public partial class ValidationSet
{
    /// <summary>
    /// Gets or sets the identifier for the validation set.
    /// </summary>
    [Key]
    [Column("id_validation_set")]
    public long IdValidationSet { get; set; }

    /// <summary>
    /// Gets or sets the name of the validation set with a maximum length of 50 characters.
    /// </summary>
    [Column("validation_name", TypeName = "VARCHAR(50)")]
    public string ValidationName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of <see cref="FieldValidation"/> entities associated with this validation set.
    /// </summary>
    [InverseProperty("IdValidationSetNavigation")]
    public virtual ICollection<FieldValidation> FieldValidations { get; set; } = new List<FieldValidation>();
}
