using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class AddUtilizadorIdToCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UtilizadorId",
                table: "Categoria",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_UtilizadorId",
                table: "Categoria",
                column: "UtilizadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_AspNetUsers_UtilizadorId",
                table: "Categoria",
                column: "UtilizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_AspNetUsers_UtilizadorId",
                table: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_UtilizadorId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "UtilizadorId",
                table: "Categoria");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
