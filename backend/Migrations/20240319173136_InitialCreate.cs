using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ganhadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdAposta = table.Column<int>(type: "INTEGER", nullable: false),
                    IdSorteio = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeGanhador = table.Column<string>(type: "TEXT", nullable: false),
                    CpfGanhador = table.Column<string>(type: "TEXT", nullable: false),
                    NumerosEscolhidos = table.Column<string>(type: "TEXT", nullable: false),
                    DataAposta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ganhadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sorteios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumerosGanhadores = table.Column<string>(type: "TEXT", nullable: false),
                    DataSorteio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorteios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apostas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    NumerosEscolhidos = table.Column<string>(type: "TEXT", nullable: true),
                    DataAposta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SorteioId = table.Column<int>(type: "INTEGER", nullable: false),
                    SorteioGanhadorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apostas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apostas_Sorteios_SorteioGanhadorId",
                        column: x => x.SorteioGanhadorId,
                        principalTable: "Sorteios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Apostas_Sorteios_SorteioId",
                        column: x => x.SorteioId,
                        principalTable: "Sorteios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apostas_SorteioGanhadorId",
                table: "Apostas",
                column: "SorteioGanhadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Apostas_SorteioId",
                table: "Apostas",
                column: "SorteioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apostas");

            migrationBuilder.DropTable(
                name: "Ganhadores");

            migrationBuilder.DropTable(
                name: "Sorteios");
        }
    }
}
