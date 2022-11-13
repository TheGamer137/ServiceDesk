using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceDesk.Migrations
{
    /// <inheritdoc />
    public partial class changedClientAndExecutorIdValueType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("34970145-fb51-4cea-83fe-623e0dddc636"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("41efe9e5-5be9-47c9-b6e6-78e906d7ece7"), 0, "302700d0-cbd1-4668-b5bb-2b6861eedb86", "User", "admin@mail.ru", false, null, null, false, null, null, null, "Qwerty123", null, null, false, 1, "9de34ab8-3e10-49e4-9291-9e2d0803737b", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("41efe9e5-5be9-47c9-b6e6-78e906d7ece7"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("34970145-fb51-4cea-83fe-623e0dddc636"), 0, "41e550f3-7d67-43bd-a5dc-fdca34f5a50c", "User", "admin@mail.ru", false, null, null, false, null, null, null, "Qwerty123", null, null, false, 1, "4e8b6503-f690-4a56-ba2d-e24b4f93e2d7", false, null });
        }
    }
}
