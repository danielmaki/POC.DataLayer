using Microsoft.EntityFrameworkCore.Migrations;

namespace POC.DataLayer.Data.Migrations
{
    public partial class Fruit_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fruits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, defaultValue: "Unknown"),
                    Taste = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fruits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fruits_Name",
                table: "Fruits",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fruits");
        }
    }
}
