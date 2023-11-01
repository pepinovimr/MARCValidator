using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "field_type",
                columns: table => new
                {
                    id_field_type = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    field_type = table.Column<string>(type: "VARCHAR(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_type", x => x.id_field_type);
                });

            migrationBuilder.CreateTable(
                name: "subfield",
                columns: table => new
                {
                    id_subfield = table.Column<long>(type: "INTEGER", nullable: false),
                    subfield_code = table.Column<string>(type: "VARCHAR(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subfield", x => x.id_subfield);
                });

            migrationBuilder.CreateTable(
                name: "validation",
                columns: table => new
                {
                    id_validation = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    validation = table.Column<string>(type: "VARCHAR(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_validation", x => x.id_validation);
                });

            migrationBuilder.CreateTable(
                name: "validation_obligation",
                columns: table => new
                {
                    id_validation_obligation = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    obligation = table.Column<string>(type: "VARCHAR(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_validation_obligation", x => x.id_validation_obligation);
                });

            migrationBuilder.CreateTable(
                name: "validation_set",
                columns: table => new
                {
                    id_validation_set = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    validation_name = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_validation_set", x => x.id_validation_set);
                });

            migrationBuilder.CreateTable(
                name: "marc_field",
                columns: table => new
                {
                    id_marc_field = table.Column<long>(type: "INTEGER", nullable: false),
                    id_field_type = table.Column<long>(type: "INTEGER", nullable: false),
                    tag = table.Column<string>(type: "VARCHAR(3)", nullable: true),
                    ind1 = table.Column<string>(type: "VARCHAR(1)", nullable: true),
                    ind2 = table.Column<string>(type: "VARCHAR(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marc_field", x => new { x.id_marc_field, x.id_field_type });
                    table.UniqueConstraint("AK_marc_field_id_marc_field", x => x.id_marc_field);
                    table.ForeignKey(
                        name: "FK_marc_field_field_type_id_field_type",
                        column: x => x.id_field_type,
                        principalTable: "field_type",
                        principalColumn: "id_field_type");
                });

            migrationBuilder.CreateTable(
                name: "field_validation",
                columns: table => new
                {
                    id_marc_field = table.Column<long>(type: "INTEGER", nullable: false),
                    id_validation_set = table.Column<long>(type: "INTEGER", nullable: false),
                    id_validation = table.Column<long>(type: "INTEGER", nullable: false),
                    id_validation_obligation = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_validation", x => new { x.id_marc_field, x.id_validation_set, x.id_validation, x.id_validation_obligation });
                    table.ForeignKey(
                        name: "FK_field_validation_marc_field_id_marc_field",
                        column: x => x.id_marc_field,
                        principalTable: "marc_field",
                        principalColumn: "id_marc_field");
                    table.ForeignKey(
                        name: "FK_field_validation_validation_id_validation",
                        column: x => x.id_validation,
                        principalTable: "validation",
                        principalColumn: "id_validation");
                    table.ForeignKey(
                        name: "FK_field_validation_validation_obligation_id_validation_obligation",
                        column: x => x.id_validation_obligation,
                        principalTable: "validation_obligation",
                        principalColumn: "id_validation_obligation");
                    table.ForeignKey(
                        name: "FK_field_validation_validation_set_id_validation_set",
                        column: x => x.id_validation_set,
                        principalTable: "validation_set",
                        principalColumn: "id_validation_set");
                });

            migrationBuilder.CreateTable(
                name: "marc_field_has_subfield",
                columns: table => new
                {
                    id_subfield = table.Column<long>(type: "INTEGER", nullable: false),
                    id_marc_field = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marc_field_has_subfield", x => x.id_subfield);
                    table.ForeignKey(
                        name: "FK_marc_field_has_subfield_marc_field_id_marc_field",
                        column: x => x.id_marc_field,
                        principalTable: "marc_field",
                        principalColumn: "id_marc_field");
                    table.ForeignKey(
                        name: "FK_marc_field_has_subfield_subfield_id_subfield",
                        column: x => x.id_subfield,
                        principalTable: "subfield",
                        principalColumn: "id_subfield");
                });

            migrationBuilder.CreateIndex(
                name: "fk_field_validation_validation_obligation1_idx",
                table: "field_validation",
                column: "id_validation_obligation");

            migrationBuilder.CreateIndex(
                name: "fk_field_validation_validation_set1_idx",
                table: "field_validation",
                column: "id_validation_set");

            migrationBuilder.CreateIndex(
                name: "fk_field_validation_validation1_idx",
                table: "field_validation",
                column: "id_validation");

            migrationBuilder.CreateIndex(
                name: "fk_MARC_field_field_type_idx",
                table: "marc_field",
                column: "id_field_type");

            migrationBuilder.CreateIndex(
                name: "id_marc_field_UNIQUE",
                table: "marc_field",
                column: "id_marc_field",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_marc_field_id_marc_field",
                table: "marc_field",
                column: "id_marc_field",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_marc_field_has_subfield_marc_field1_idx",
                table: "marc_field_has_subfield",
                column: "id_marc_field");

            migrationBuilder.CreateIndex(
                name: "validation_name_UNIQUE",
                table: "validation_set",
                column: "validation_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "field_validation");

            migrationBuilder.DropTable(
                name: "marc_field_has_subfield");

            migrationBuilder.DropTable(
                name: "validation");

            migrationBuilder.DropTable(
                name: "validation_obligation");

            migrationBuilder.DropTable(
                name: "validation_set");

            migrationBuilder.DropTable(
                name: "marc_field");

            migrationBuilder.DropTable(
                name: "subfield");

            migrationBuilder.DropTable(
                name: "field_type");
        }
    }
}
