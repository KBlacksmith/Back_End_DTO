using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_Peliculas.Migrations
{
    public partial class Critica_Usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Criticas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Criticas_UsuarioId",
                table: "Criticas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Criticas_AspNetUsers_UsuarioId",
                table: "Criticas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criticas_AspNetUsers_UsuarioId",
                table: "Criticas");

            migrationBuilder.DropIndex(
                name: "IX_Criticas_UsuarioId",
                table: "Criticas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Criticas");
        }
    }
}
