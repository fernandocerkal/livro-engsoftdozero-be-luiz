using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Botsuana.Covid19.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dose",
                columns: table => new
                {
                    Identificador = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dose", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Vacinado",
                columns: table => new
                {
                    Identificador = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CPF = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    dataHora = table.Column<DateTime>(nullable: false),
                    doseIdentificador = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacinado", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_Vacinado_Dose_doseIdentificador",
                        column: x => x.doseIdentificador,
                        principalTable: "Dose",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacinado_CPF",
                table: "Vacinado",
                column: "CPF");

            migrationBuilder.CreateIndex(
                name: "IX_Vacinado_doseIdentificador",
                table: "Vacinado",
                column: "doseIdentificador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacinado");

            migrationBuilder.DropTable(
                name: "Dose");
        }
    }
}
