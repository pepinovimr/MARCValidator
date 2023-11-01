using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModels;

/// <summary>
/// Represents all <see cref="Validation"/> and its <see cref="ValidationObligation"/> for each <see cref="MarcField"/> according to different <see cref="ValidationSet"/>.
/// </summary>
[PrimaryKey("IdMarcField", "IdValidationSet", "IdValidation", "IdValidationObligation")]
[Table("field_validation")]
[Index("IdValidation", Name = "fk_field_validation_validation1_idx")]
[Index("IdValidationObligation", Name = "fk_field_validation_validation_obligation1_idx")]
[Index("IdValidationSet", Name = "fk_field_validation_validation_set1_idx")]
public partial class FieldValidation
{
    /// <summary>
    /// Gets or sets the identifier for the <see cref="MarcField"/> in the validation.
    /// </summary>
    [Key]
    [Column("id_marc_field")]
    public long IdMarcField { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the <see cref="ValidationSet"/> associated with the field.
    /// </summary>
    [Key]
    [Column("id_validation_set")]
    public long IdValidationSet { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the <see cref="Validation"/> associated with the field.
    /// </summary>
    [Key]
    [Column("id_validation")]
    public long IdValidation { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the <see cref="ValidationObligation"/> associated with the field.
    /// </summary>
    [Key]
    [Column("id_validation_obligation")]
    public long IdValidationObligation { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for the <see cref="MarcField"/> associated with this validation.
    /// </summary>
    public virtual MarcField IdMarcFieldNavigation { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation property for the <see cref="Validation"/> associated with this field.
    /// </summary>
    [ForeignKey("IdValidation")]
    [InverseProperty("FieldValidations")]
    public virtual Validation IdValidationNavigation { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation property for the <see cref="ValidationObligation"/> associated with this field.
    /// </summary>
    [ForeignKey("IdValidationObligation")]
    [InverseProperty("FieldValidations")]
    public virtual ValidationObligation IdValidationObligationNavigation { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation property for the <see cref="ValidationSet"/> associated with this field.
    /// </summary>
    [ForeignKey("IdValidationSet")]
    [InverseProperty("FieldValidations")]
    public virtual ValidationSet IdValidationSetNavigation { get; set; } = null!;
}