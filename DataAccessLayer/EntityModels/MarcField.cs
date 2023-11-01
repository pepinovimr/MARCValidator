using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModels;

/// <summary>
/// Represents each MARC field.
/// </summary>
[PrimaryKey("IdMarcField", "IdFieldType")]
[Table("marc_field")]
[Index("IdMarcField", IsUnique = true)]
[Index("IdFieldType", Name = "fk_MARC_field_field_type_idx")]
[Index("IdMarcField", Name = "id_marc_field_UNIQUE", IsUnique = true)]
public partial class MarcField
{
    /// <summary>
    /// Gets or sets the identifier for the MARC field.
    /// </summary>
    [Key]
    [Column("id_marc_field")]
    public long IdMarcField { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the field type associated with the MARC field.
    /// </summary>
    [Key]
    [Column("id_field_type")]
    public long IdFieldType { get; set; }

    /// <summary>
    /// Gets or sets the tag for the MARC field with a maximum length of 3 characters.
    /// </summary>
    [Column("tag", TypeName = "VARCHAR(3)")]
    public string? Tag { get; set; }

    /// <summary>
    /// Gets or sets the first indicator for the MARC field with a maximum length of 1 character.
    /// </summary>
    [Column("ind1", TypeName = "VARCHAR(1)")]
    public string? Ind1 { get; set; }

    /// <summary>
    /// Gets or sets the second indicator for the MARC field with a maximum length of 1 character.
    /// </summary>
    [Column("ind2", TypeName = "VARCHAR(1)")]
    public string? Ind2 { get; set; }

    /// <summary>
    /// Gets or sets the collection of <see cref="FieldValidation"/> entities associated with this MARC field.
    /// </summary>
    public virtual ICollection<FieldValidation> FieldValidations { get; set; } = new List<FieldValidation>();

    /// <summary>
    /// Gets or sets the navigation property for the <see cref="FieldType"/> associated with this MARC field.
    /// </summary>
    [ForeignKey("IdFieldType")]
    [InverseProperty("MarcFields")]
    public virtual FieldType IdFieldTypeNavigation { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of <see cref="MarcFieldHasSubfield"/> entities associated with this MARC field.
    /// </summary>
    public virtual ICollection<MarcFieldHasSubfield> MarcFieldHasSubfields { get; set; } = new List<MarcFieldHasSubfield>();
}
