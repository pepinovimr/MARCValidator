using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EntityModels;

/// <summary>
/// Represents type of each <see cref="MarcField"/>
/// </summary>
[Table("field_type")]
public partial class FieldType
{
    /// <summary>
    /// Gets or sets the unique identifier for the field type.
    /// </summary>
    [Key]
    [Column("id_field_type")]
    public long IdFieldType { get; set; }

    /// <summary>
    /// Gets or sets the field type with a maximum length of 20 characters.
    /// </summary>
    [Column("field_type", TypeName = "VARCHAR(20)")]
    public string FieldType1 { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of <see cref="MarcField"/>s associated with this field type.
    /// </summary>
    [InverseProperty("IdFieldTypeNavigation")]
    public virtual ICollection<MarcField> MarcFields { get; set; } = new List<MarcField>();
}

