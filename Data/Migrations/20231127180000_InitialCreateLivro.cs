using Microsoft.EntityFrameworkCore.Migrations;

namespace LivroApi.Api.Data.Migrations
{
    public partial class InitialCreateLivro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    LivroID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(maxLength: 100, nullable: false),
                    DataPublicacao = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    AutorID = table.Column<int>(nullable: false),
                    AssuntoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.LivroID);
                    table.ForeignKey(
                        name: "FK_Livros_Autores_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Autores",
                        principalColumn: "AutorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livros_Assuntos_AssuntoID",
                        column: x => x.AssuntoID,
                        principalTable: "Assuntos",
                        principalColumn: "AssuntoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AutorID",
                table: "Livros",
                column: "AutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AssuntoID",
                table: "Livros",
                column: "AssuntoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
