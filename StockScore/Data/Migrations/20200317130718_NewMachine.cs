using Microsoft.EntityFrameworkCore.Migrations;

namespace StockScore.Data.Migrations
{
    public partial class NewMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Stocks_AspNetUsers_UserId",
                table: "User_Stocks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b47466fe-1b44-4404-80b2-b80a1ab1d081");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "User_Stocks",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Searches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Favorites",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "721bdd3b-42d7-409e-b911-225afb4b6ed0", "dfbe8099-55bc-4110-baf5-dff48eb954a4", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Searches_UserId",
                table: "Searches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserId",
                table: "User",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_User_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Searches_User_UserId",
                table: "Searches",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Stocks_User_UserId",
                table: "User_Stocks",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_User_UserId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Searches_User_UserId",
                table: "Searches");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Stocks_User_UserId",
                table: "User_Stocks");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Searches_UserId",
                table: "Searches");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "721bdd3b-42d7-409e-b911-225afb4b6ed0");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Searches");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "User_Stocks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favorites",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b47466fe-1b44-4404-80b2-b80a1ab1d081", "cc2c3a96-e2ed-40fa-baac-d2868fcc0a83", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Stocks_AspNetUsers_UserId",
                table: "User_Stocks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
