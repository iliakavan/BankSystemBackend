using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changingTransactionHitoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: new Guid("02cc1245-3462-4411-8030-b39dccd75248"));

            migrationBuilder.DropColumn(
                name: "DestAccountID",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "Job",
                table: "TransactionHistory");

            migrationBuilder.AddColumn<string>(
                name: "DestCreditNumber",
                table: "TransactionHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginCreditNumber",
                table: "TransactionHistory",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestCreditNumber",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "OrginCreditNumber",
                table: "TransactionHistory");

            migrationBuilder.AddColumn<Guid>(
                name: "DestAccountID",
                table: "TransactionHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "TransactionHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                values: new object[] { new Guid("02cc1245-3462-4411-8030-b39dccd75248"), "Admin@Admin.com", "Admin-FirstName", false, "Admin-LastName", "$2a$11$dUzm8grFczegu26wPFgjh.VTVfo8oiwR6USL8cozRCcjOxQsMQUVa", new Guid("00000000-0000-0000-0000-000000000000"), "Admin-UserName" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 2, new Guid("02cc1245-3462-4411-8030-b39dccd75248") },
                    { 2, 1, new Guid("02cc1245-3462-4411-8030-b39dccd75248") }
                });
        }
    }
}
