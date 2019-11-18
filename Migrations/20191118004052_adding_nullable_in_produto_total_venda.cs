using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleDeEstoque.Migrations
{
    public partial class adding_nullable_in_produto_total_venda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_TotalVendas_TotalVendaId",
                table: "Produtos");

            migrationBuilder.AlterColumn<int>(
                name: "TotalVendaId",
                table: "Produtos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_TotalVendas_TotalVendaId",
                table: "Produtos",
                column: "TotalVendaId",
                principalTable: "TotalVendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_TotalVendas_TotalVendaId",
                table: "Produtos");

            migrationBuilder.AlterColumn<int>(
                name: "TotalVendaId",
                table: "Produtos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_TotalVendas_TotalVendaId",
                table: "Produtos",
                column: "TotalVendaId",
                principalTable: "TotalVendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
