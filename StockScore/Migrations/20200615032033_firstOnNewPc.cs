using Microsoft.EntityFrameworkCore.Migrations;

namespace StockScore.Migrations
{
    public partial class firstOnNewPc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d645f7d-98fb-4cf7-a68a-6ba16780bc7c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc58929f-df3f-4d34-8900-5e5cdbaaed06", "65afe1a2-649b-4cac-b497-45c94e11ee67", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc58929f-df3f-4d34-8900-5e5cdbaaed06");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4d645f7d-98fb-4cf7-a68a-6ba16780bc7c", "11430f39-07ea-4c67-b9e9-afc7b5e4232b", "User", "USER" });
        }
    }
}
