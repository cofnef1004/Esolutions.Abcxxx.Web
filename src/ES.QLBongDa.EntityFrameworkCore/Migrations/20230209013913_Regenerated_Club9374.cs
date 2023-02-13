using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES.QLBongDa.Migrations
{
    public partial class Regenerated_Club9374 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MACLB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TENCLB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MASAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MATINH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StadiumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_StadiumId",
                table: "Clubs",
                column: "StadiumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clubs");
        }
    }
}
