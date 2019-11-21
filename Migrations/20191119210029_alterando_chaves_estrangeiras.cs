using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleDeEstoque.Migrations
{
    public partial class alterando_chaves_estrangeiras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_TotalVendas_TotalVendaId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_TotalVendaId",
                table: "Produtos");

            migrationBuilder.CreateIndex(
                name: "IX_TotalVendas_ProdutoId",
                table: "TotalVendas",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TotalVendas_Produtos_ProdutoId",
                table: "TotalVendas",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TotalVendas_Produtos_ProdutoId",
                table: "TotalVendas");

            migrationBuilder.DropIndex(
                name: "IX_TotalVendas_ProdutoId",
                table: "TotalVendas");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_TotalVendaId",
                table: "Produtos",
                column: "TotalVendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_TotalVendas_TotalVendaId",
                table: "Produtos",
                column: "TotalVendaId",
                principalTable: "TotalVendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
