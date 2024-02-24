using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SjcVotersPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentAssociationModelAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentAssociations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RollNumber = table.Column<string>(type: "TEXT", nullable: false),
                    AssociationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssociations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssociations_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssociations_Students_RollNumber",
                        column: x => x.RollNumber,
                        principalTable: "Students",
                        principalColumn: "RollNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssociations_AssociationId",
                table: "StudentAssociations",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssociations_RollNumber",
                table: "StudentAssociations",
                column: "RollNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAssociations");
        }
    }
}
