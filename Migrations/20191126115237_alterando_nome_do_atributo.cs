using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleDeEstoque.Migrations
{
    public partial class alterando_nome_do_atributo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVendaId",
                table: "Produto");

            migrationBuilder.AddColumn<int>(
                name: "SaidaEstoqueId",
                table: "Produto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaidaEstoqueId",
                table: "Produto");

            migrationBuilder.AddColumn<int>(
                name: "TotalVendaId",
                table: "Produto",
                type: "int",
                nullable: true);
        }
    }
}
