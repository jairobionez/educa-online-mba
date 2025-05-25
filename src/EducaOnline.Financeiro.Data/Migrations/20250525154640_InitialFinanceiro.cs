using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducaOnline.Financeiro.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialFinanceiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CursoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlunoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ValorPago = table.Column<decimal>(type: "TEXT", nullable: false),
                    DadosCartao_NomeCartao = table.Column<string>(type: "TEXT", nullable: false),
                    DadosCartao_NumeroCartao = table.Column<string>(type: "TEXT", nullable: false),
                    DadosCartao_ExpiracaoCartao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DadosCartao_CvvCartao = table.Column<string>(type: "TEXT", nullable: false),
                    StatusPagamento_Codigo = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusPagamento_Descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamento");
        }
    }
}
