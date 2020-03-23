using Microsoft.EntityFrameworkCore.Migrations;

namespace StockScore.Migrations
{
    public partial class AccidentallyDeletedATable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da128cab-d4e4-41da-aa20-e2a9fa6e0bfc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59553c4e-1537-4860-ae00-59d1dc66b13e", "c09fef82-4348-4ba6-8091-4aa2a279901f", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59553c4e-1537-4860-ae00-59d1dc66b13e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da128cab-d4e4-41da-aa20-e2a9fa6e0bfc", "3b92c273-3e43-4962-ba50-2d3006842bf7", "User", "USER" });
        }
    }
}
