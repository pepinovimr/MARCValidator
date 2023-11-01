using System;
using System.Collections.Generic;
using DataAccessLayer.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

/// <summary>
/// Represents the database context for interacting with the MARC Validator database.
/// </summary>
public partial class MarcValidatorContext : DbContext
{
    //Default project: DataAccessLayer
    //Scaffold-DbContext "DataSource=.\Database\MARC_Validator.db" Microsoft.EntityFrameworkCore.Sqlite -DataAnnotations -ContextDir .\Context -OutputDir .\EntityModels
    public MarcValidatorContext()
    {
    }

    public MarcValidatorContext(DbContextOptions<MarcValidatorContext> options)
        : base(options)
    {
    }
    /// <summary>
    /// Gets or sets the DbSet of <see cref="FieldType"/> representing a table in the database.
    /// </summary>
    public virtual DbSet<FieldType> FieldTypes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of <see cref="FieldValidation"/> representing a table in the database.
    /// </summary>
    public virtual DbSet<FieldValidation> FieldValidations { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of <see cref="MarcField"/> representing a table in the database.
    /// </summary>
    public virtual DbSet<MarcField> MarcFields { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of <see cref="MarcFieldHasSubfield"/> representing a table in the database.
    /// </summary>
    public virtual DbSet<MarcFieldHasSubfield> MarcFieldHasSubfields { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of <see cref="Subfield"/> representing a table in the database.
    /// </summary>
    public virtual DbSet<Subfield> Subfields { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of <see cref="Validation"/> representing a table in the database.
    /// </summary>
    public virtual DbSet<Validation> Validations { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of <see cref="ValidationObligation"/> representing a table in the database.
    /// </summary>
    public virtual DbSet<ValidationObligation> ValidationObligations { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of <see cref="ValidationSet"/> representing a table in the database.
    /// </summary>
    public virtual DbSet<ValidationSet> ValidationSets { get; set; }

    /// <summary>
    /// Configures the database connection for this context.
    /// </summary>
    /// <param name="optionsBuilder">The builder used to create or modify options for this context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=.\\Database\\MARC_Validator.db");
    /// <summary>
    /// Called when the model for a derived context has been initialized.
    /// </summary>
    /// <param name="modelBuilder">Defines the shape of the entities, their relationships, and mappings.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FieldValidation>(entity =>
        {
            entity.HasOne(d => d.IdMarcFieldNavigation).WithMany(p => p.FieldValidations)
                .HasPrincipalKey(p => p.IdMarcField)
                .HasForeignKey(d => d.IdMarcField)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdValidationNavigation).WithMany(p => p.FieldValidations).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdValidationObligationNavigation).WithMany(p => p.FieldValidations).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdValidationSetNavigation).WithMany(p => p.FieldValidations).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MarcField>(entity =>
        {
            entity.HasOne(d => d.IdFieldTypeNavigation).WithMany(p => p.MarcFields).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MarcFieldHasSubfield>(entity =>
        {
            entity.Property(e => e.IdSubfield).ValueGeneratedNever();

            entity.HasOne(d => d.IdMarcFieldNavigation).WithMany(p => p.MarcFieldHasSubfields)
                .HasPrincipalKey(p => p.IdMarcField)
                .HasForeignKey(d => d.IdMarcField)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdSubfieldNavigation).WithOne(p => p.MarcFieldHasSubfield).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Subfield>(entity =>
        {
            entity.Property(e => e.IdSubfield).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }
    /// <summary>
    /// Partial method allowing customization of the model building process.
    /// </summary>
    /// <param name="modelBuilder">Defines the shape of the entities, their relationships, and mappings.</param>
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
