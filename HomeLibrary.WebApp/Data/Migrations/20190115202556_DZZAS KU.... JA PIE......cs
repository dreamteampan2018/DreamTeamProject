using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeLibrary.WebApp.Data.Migrations
{
    public partial class DZZASKUJAPIE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Statistics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Statistics",
                nullable: false,
                defaultValue: 0);
        }
    }
}
