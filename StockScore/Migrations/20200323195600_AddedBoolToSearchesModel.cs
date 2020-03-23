using Microsoft.EntityFrameworkCore.Migrations;

namespace StockScore.Migrations
{
    public partial class AddedBoolToSearchesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0015586-2d7c-422b-8fab-87590baccf22");

            migrationBuilder.AddColumn<bool>(
                name: "IsForPastScores",
                table: "Searches",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da128cab-d4e4-41da-aa20-e2a9fa6e0bfc", "3b92c273-3e43-4962-ba50-2d3006842bf7", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da128cab-d4e4-41da-aa20-e2a9fa6e0bfc");

            migrationBuilder.DropColumn(
                name: "IsForPastScores",
                table: "Searches");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0015586-2d7c-422b-8fab-87590baccf22", "61dc6d0b-d6ea-417a-bc56-fd65162247ad", "User", "USER" });
        }
    }
}
