using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarteiraCerta.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarteiraCerta_Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarteiraCerta_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "CarteiraCerta_Carteiras",
                columns: table => new
                {
                    IdCarteira = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NomeCarteira = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarteiraCerta_Carteiras", x => x.IdCarteira);
                    table.ForeignKey(
                        name: "FK_CarteiraCerta_Carteiras_CarteiraCerta_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "CarteiraCerta_Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarteiraCerta_Ativos",
                columns: table => new
                {
                    IdAtivo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Ticker = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NomeEmpresa = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PrecoAtual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarteiraIdCarteira = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarteiraCerta_Ativos", x => x.IdAtivo);
                    table.ForeignKey(
                        name: "FK_CarteiraCerta_Ativos_CarteiraCerta_Carteiras_CarteiraIdCarteira",
                        column: x => x.CarteiraIdCarteira,
                        principalTable: "CarteiraCerta_Carteiras",
                        principalColumn: "IdCarteira");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarteiraCerta_Ativos_CarteiraIdCarteira",
                table: "CarteiraCerta_Ativos",
                column: "CarteiraIdCarteira");

            migrationBuilder.CreateIndex(
                name: "IX_CarteiraCerta_Carteiras_IdUsuario",
                table: "CarteiraCerta_Carteiras",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarteiraCerta_Ativos");

            migrationBuilder.DropTable(
                name: "CarteiraCerta_Carteiras");

            migrationBuilder.DropTable(
                name: "CarteiraCerta_Usuarios");
        }
    }
}
