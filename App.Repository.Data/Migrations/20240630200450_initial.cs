using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repository.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sobrenome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SysDateInsert = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SysDateUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SysUserInsert = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SysUserUpdate = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RepositorioId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UsuariosId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SysDateInsert = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SysDateUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SysUserInsert = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SysUserUpdate = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favoritos_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Repositorios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Linguagem = table.Column<int>(type: "int", nullable: true),
                    Incluido = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UsuariosId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SysDateInsert = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SysDateUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SysUserInsert = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SysUserUpdate = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositorios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repositorios_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UsuariosId",
                table: "Favoritos",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositorios_UsuariosId",
                table: "Repositorios",
                column: "UsuariosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "Repositorios");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
