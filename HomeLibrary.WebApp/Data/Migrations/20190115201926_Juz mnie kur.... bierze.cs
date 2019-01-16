using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeLibrary.WebApp.Data.Migrations
{
    public partial class Juzmniekurbierze : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
