using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockScore.Migrations
{
    public partial class Removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59553c4e-1537-4860-ae00-59d1dc66b13e");

            migrationBuilder.DropColumn(
                name: "date",
                table: "Top_Stocks");

            migrationBuilder.DropColumn(
                name: "StockData",
                table: "Searches");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc3440e5-28ec-4670-bfa6-843b9c90dfd4", "224d5496-85f8-4a4d-8194-65310c3f9492", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc3440e5-28ec-4670-bfa6-843b9c90dfd4");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "Top_Stocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StockData",
                table: "Searches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59553c4e-1537-4860-ae00-59d1dc66b13e", "c09fef82-4348-4ba6-8091-4aa2a279901f", "User", "USER" });
        }
    }
}
