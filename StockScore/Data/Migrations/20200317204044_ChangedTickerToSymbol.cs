using Microsoft.EntityFrameworkCore.Migrations;

namespace StockScore.Data.Migrations
{
    public partial class ChangedTickerToSymbol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5e831be-4baf-45aa-945a-ac2ffa71458a");

            migrationBuilder.DropColumn(
                name: "DateSearched",
                table: "Searches");

            migrationBuilder.DropColumn(
                name: "Ticker",
                table: "Searches");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Searches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeFrame",
                table: "Searches",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2b9dad0c-a267-4fce-adc1-6195c60b19b1", "4fd2ae6d-8d99-401c-9a88-ee4f59578584", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b9dad0c-a267-4fce-adc1-6195c60b19b1");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Searches");

            migrationBuilder.DropColumn(
                name: "TimeFrame",
                table: "Searches");

            migrationBuilder.AddColumn<string>(
                name: "DateSearched",
                table: "Searches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ticker",
                table: "Searches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d5e831be-4baf-45aa-945a-ac2ffa71458a", "483661f5-fbfd-49e7-af7b-b21dd34a4bdd", "User", "USER" });
        }
    }
}
