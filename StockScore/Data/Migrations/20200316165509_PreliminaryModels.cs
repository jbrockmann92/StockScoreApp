using Microsoft.EntityFrameworkCore.Migrations;

namespace StockScore.Data.Migrations
{
    public partial class PreliminaryModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2890d0f-8a1c-4e90-ba37-118aea86e654");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6fc8d4d-0b90-46dd-8308-eb1e3fcbc0c6", "a25dff9e-44cd-4794-ac55-a7d809e658d0", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6fc8d4d-0b90-46dd-8308-eb1e3fcbc0c6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a2890d0f-8a1c-4e90-ba37-118aea86e654", "f02b3f4f-ba8a-4962-a688-998934b82b9e", "User", "USER" });
        }
    }
}
