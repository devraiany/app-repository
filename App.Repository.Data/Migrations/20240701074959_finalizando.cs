using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repository.Data.Migrations
{
    public partial class finalizando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Incluido",
                table: "Repositorios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Incluido",
                table: "Repositorios",
                type: "tinyint(1)",
                nullable: true);
        }
    }
}
