using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Email", "First_Name", "IsDelete", "Last_Name", "Password", "RefreshTokenID", "UserName" },
                values: new object[] { new Guid("b44b7fd7-fa37-415f-a7f6-723008802408"), "Admin@Admin.com", "Admin-FirstName", false, "Admin-LastName", "$2a$11$HPZl/O2VyPGC/SK0qCrCOeJnjQwQpOXzf8jlcdkKfVwi2372tY6qC", new Guid("00000000-0000-0000-0000-000000000000"), "Admin-UserName" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 2, new Guid("b44b7fd7-fa37-415f-a7f6-723008802408") },
                    { 2, 1, new Guid("b44b7fd7-fa37-415f-a7f6-723008802408") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("b44b7fd7-fa37-415f-a7f6-723008802408"));
        }
    }
}
