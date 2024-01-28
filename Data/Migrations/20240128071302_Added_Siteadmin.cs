using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SjcVotersPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Siteadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {   
            migrationBuilder.Sql("""INSERT INTO "AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES('22f94601-91aa-4006-8c0f-42e452d1e74c', 'SiteAdmin', 'SITEADMIN', 'bf767acd-0378-4255-b92c-913af2765e1e');""");         
            migrationBuilder.Sql("""INSERT INTO "AspNetUsers" ("Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName") VALUES ('4b1a46d3-cac0-4f4e-a505-a14ccc3f2c8c', '0', 'd0f94113-ec54-4733-af29-f115f50e4e6a', 'marvelred20@gmail.com', '1', '1', NULL, 'MARVELRED20@GMAIL.COM', 'MARVELRED20@GMAIL.COM', 'AQAAAAIAAYagAAAAEEzwl8SCbo8VIBy3hpgSC/ZihPf8jFi+71r4+IL1dDxMqXIgm8Rj6LEb8IGHlBMZOw==', NULL, '0', 'QVTCSJYZL6WZ62P2HFZFSUIBR7ZSAGMA', '0', 'marvelred20@gmail.com');""");            
            migrationBuilder.Sql("""INSERT INTO "AspNetUserRoles" ("UserId", "RoleId") VALUES('4b1a46d3-cac0-4f4e-a505-a14ccc3f2c8c', '22f94601-91aa-4006-8c0f-42e452d1e74c');""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
