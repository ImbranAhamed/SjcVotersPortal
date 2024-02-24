using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SjcVotersPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentModelRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    RollNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Batch = table.Column<string>(type: "TEXT", nullable: false),
                    CourseId = table.Column<string>(type: "TEXT", nullable: false),
                    EmailId = table.Column<string>(type: "TEXT", nullable: false),
                    IdCarFileMime = table.Column<string>(type: "TEXT", nullable: false),
                    IdCardFile = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.RollNumber);
                });
        }
    }
}
