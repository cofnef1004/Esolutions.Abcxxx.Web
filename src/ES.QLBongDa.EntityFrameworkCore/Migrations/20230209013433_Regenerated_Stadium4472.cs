using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES.QLBongDa.Migrations
{
    public partial class Regenerated_Stadium4472 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bpCoach");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.CreateTable(
                name: "Stadiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Masan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tensan = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadiums", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stadiums");

            migrationBuilder.CreateTable(
                name: "bpCoach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DIENTHOAI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MAHLV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MAQUOCGIA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TENHLV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bpCoach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MACLB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MASAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MATINH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TENCLB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bpCoach_TenantId",
                table: "bpCoach",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_TenantId",
                table: "Clubs",
                column: "TenantId");
        }
    }
}
