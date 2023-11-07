using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModels;
/// <summary>
/// Represents each possible <see cref="Subfield"/> for each <see cref="MarcField"/> table.
/// </summary>
[Table("marc_field_has_subfield")]
[Index("IdMarcField", Name = "fk_marc_field_has_subfield_marc_field1_idx")]
public partial class MarcFieldHasSubfield
{
    /// <summary>
    /// Gets or sets the identifier for the <see cref="Subfield"/>.
    /// </summary>
    [Key]
    [Column("id_subfield")]
    public long IdSubfield { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the associated <see cref="MarcField"/>.
    /// </summary>
    [Column("id_marc_field")]
    public long IdMarcField { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for the <see cref="MarcField"/> associated with this subfield.
    /// </summary>
    public virtual MarcField IdMarcFieldNavigation { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation property for the <see cref="Subfield"/> associated with this relationship.
    /// </summary>
    [ForeignKey("IdSubfield")]
    [InverseProperty("MarcFieldHasSubfield")]
    public virtual Subfield IdSubfieldNavigation { get; set; } = null!;
}

