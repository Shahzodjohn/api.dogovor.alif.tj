using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectionProvider.Migrations
{
    public partial class AddingArchiveValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "Archives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Archives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Archives");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Archives");
        }
    }
}
