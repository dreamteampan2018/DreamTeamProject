using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeLibrary.WebApp.Data.Migrations
{
    public partial class Changecovergidtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoverGuid",
                table: "Items",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CoverGuid",
                table: "Items",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
