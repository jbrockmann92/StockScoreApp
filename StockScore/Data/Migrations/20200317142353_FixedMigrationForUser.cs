using Microsoft.EntityFrameworkCore.Migrations;

namespace StockScore.Data.Migrations
{
    public partial class FixedMigrationForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "721bdd3b-42d7-409e-b911-225afb4b6ed0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d5e831be-4baf-45aa-945a-ac2ffa71458a", "483661f5-fbfd-49e7-af7b-b21dd34a4bdd", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5e831be-4baf-45aa-945a-ac2ffa71458a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "721bdd3b-42d7-409e-b911-225afb4b6ed0", "dfbe8099-55bc-4110-baf5-dff48eb954a4", "User", "USER" });
        }
    }
}
