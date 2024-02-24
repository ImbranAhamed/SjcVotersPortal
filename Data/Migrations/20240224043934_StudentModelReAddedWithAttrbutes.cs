using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SjcVotersPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentModelReAddedWithAttrbutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    RollNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    EmailId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Batch = table.Column<string>(type: "TEXT", maxLength: 7, nullable: false),
                    CourseId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    IdCardFile = table.Column<byte[]>(type: "BLOB", nullable: false),
                    IdCarFileMime = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.RollNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
