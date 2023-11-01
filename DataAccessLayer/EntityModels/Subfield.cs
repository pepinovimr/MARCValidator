using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModels;

/// <summary>
/// Representsall possible subfields of all <see cref="MarcField"/>.
/// </summary>
[Table("subfield")]
public partial class Subfield
{
    /// <summary>
    /// Gets or sets the identifier for the subfield.
    /// </summary>
    [Key]
    [Column("id_subfield")]
    public long IdSubfield { get; set; }

    /// <summary>
    /// Gets or sets the code associated with the subfield with a maximum length of 1 character.
    /// </summary>
    [Column("subfield_code", TypeName = "VARCHAR(1)")]
    public string SubfieldCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the navigation property for the relationship with <see cref="MarcFieldHasSubfield."/>
    /// </summary>
    [InverseProperty("IdSubfieldNavigation")]
    public virtual MarcFieldHasSubfield? MarcFieldHasSubfield { get; set; }
}
