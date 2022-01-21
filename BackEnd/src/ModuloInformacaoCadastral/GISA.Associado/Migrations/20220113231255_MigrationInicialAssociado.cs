using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GISA.Associado.Migrations
{
    public partial class MigrationInicialAssociado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarcacaoExames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataExame = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoExame = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcacaoExames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPlano = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPlano = table.Column<int>(type: "int", nullable: false),
                    ClassificacaoPlano = table.Column<int>(type: "int", nullable: false),
                    ValorBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Associados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CpfCnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    Formacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanoId = table.Column<int>(type: "int", nullable: false),
                    CodigoAssociado = table.Column<int>(type: "int", nullable: false),
                    ValorPlano = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PossuiPlanoOdontologico = table.Column<bool>(type: "bit", nullable: false),
                    SituacaoAssociado = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Associados_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Associados_Planos_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssociadoMarcacaoExame",
                columns: table => new
                {
                    AssociadosId = table.Column<int>(type: "int", nullable: false),
                    MarcacaoExamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociadoMarcacaoExame", x => new { x.AssociadosId, x.MarcacaoExamesId });
                    table.ForeignKey(
                        name: "FK_AssociadoMarcacaoExame_Associados_AssociadosId",
                        column: x => x.AssociadosId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociadoMarcacaoExame_MarcacaoExames_MarcacaoExamesId",
                        column: x => x.MarcacaoExamesId,
                        principalTable: "MarcacaoExames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociadoMarcacaoExame_MarcacaoExamesId",
                table: "AssociadoMarcacaoExame",
                column: "MarcacaoExamesId");

            migrationBuilder.CreateIndex(
                name: "IX_Associados_EnderecoId",
                table: "Associados",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Associados_PlanoId",
                table: "Associados",
                column: "PlanoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociadoMarcacaoExame");

            migrationBuilder.DropTable(
                name: "Associados");

            migrationBuilder.DropTable(
                name: "MarcacaoExames");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Planos");
        }
    }
}
