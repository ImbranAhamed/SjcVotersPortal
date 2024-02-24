using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SjcVotersPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""INSERT INTO "AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES('c13c893e-0de6-4192-a199-cdbe25544625', 'Student', 'STUDENT', '96f4c96a-0e68-4549-a310-19842b75044e');""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
