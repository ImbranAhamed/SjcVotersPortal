using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SjcVotersPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedApprovalStatusInStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Students");
        }
    }
}
