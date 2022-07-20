using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectionProvider.Migrations
{
    public partial class Archive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutorsEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutorsFullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archives", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archives");
        }
    }
}
