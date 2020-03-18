using Microsoft.EntityFrameworkCore.Migrations;

namespace StockScore.Data.Migrations
{
    public partial class ChangedStockDataClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b9dad0c-a267-4fce-adc1-6195c60b19b1");

            migrationBuilder.AddColumn<string>(
                name: "StockData",
                table: "Searches",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93043e07-e2f8-43b0-9a92-30de6b766e4b", "4a785fdf-37b3-41c6-8770-bb9474b12031", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93043e07-e2f8-43b0-9a92-30de6b766e4b");

            migrationBuilder.DropColumn(
                name: "StockData",
                table: "Searches");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2b9dad0c-a267-4fce-adc1-6195c60b19b1", "4fd2ae6d-8d99-401c-9a88-ee4f59578584", "User", "USER" });
        }
    }
}
