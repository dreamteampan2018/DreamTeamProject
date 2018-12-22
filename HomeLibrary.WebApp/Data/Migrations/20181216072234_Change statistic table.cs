using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeLibrary.WebApp.Data.Migrations
{
    public partial class Changestatistictable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Statistics");

            migrationBuilder.AddColumn<string>(
                name: "ChangedUser",
                table: "Statistics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedUser",
                table: "Statistics");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Statistics",
                nullable: false,
                defaultValue: 0);
        }
    }
}
