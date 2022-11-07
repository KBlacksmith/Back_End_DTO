using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_Peliculas.Migrations
{
    public partial class DirectoresPeliculas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectorPelicas_Directores_DirectorId",
                table: "DirectorPelicas");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorPelicas_Peliculas_PeliculaId",
                table: "DirectorPelicas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirectorPelicas",
                table: "DirectorPelicas");

            migrationBuilder.RenameTable(
                name: "DirectorPelicas",
                newName: "DirectoresPeliculas");

            migrationBuilder.RenameIndex(
                name: "IX_DirectorPelicas_PeliculaId",
                table: "DirectoresPeliculas",
                newName: "IX_DirectoresPeliculas_PeliculaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirectoresPeliculas",
                table: "DirectoresPeliculas",
                columns: new[] { "DirectorId", "PeliculaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DirectoresPeliculas_Directores_DirectorId",
                table: "DirectoresPeliculas",
                column: "DirectorId",
                principalTable: "Directores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectoresPeliculas_Peliculas_PeliculaId",
                table: "DirectoresPeliculas",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectoresPeliculas_Directores_DirectorId",
                table: "DirectoresPeliculas");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectoresPeliculas_Peliculas_PeliculaId",
                table: "DirectoresPeliculas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirectoresPeliculas",
                table: "DirectoresPeliculas");

            migrationBuilder.RenameTable(
                name: "DirectoresPeliculas",
                newName: "DirectorPelicas");

            migrationBuilder.RenameIndex(
                name: "IX_DirectoresPeliculas_PeliculaId",
                table: "DirectorPelicas",
                newName: "IX_DirectorPelicas_PeliculaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirectorPelicas",
                table: "DirectorPelicas",
                columns: new[] { "DirectorId", "PeliculaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorPelicas_Directores_DirectorId",
                table: "DirectorPelicas",
                column: "DirectorId",
                principalTable: "Directores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorPelicas_Peliculas_PeliculaId",
                table: "DirectorPelicas",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
