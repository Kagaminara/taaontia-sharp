using Microsoft.EntityFrameworkCore.Migrations;

namespace Discord_Bot.Migrations
{
    public partial class FiendType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FiendTypeId",
                table: "Fiend",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FiendType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    BaseHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseEnergy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiendType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fiend_FiendTypeId",
                table: "Fiend",
                column: "FiendTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fiend_FiendType_FiendTypeId",
                table: "Fiend",
                column: "FiendTypeId",
                principalTable: "FiendType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fiend_FiendType_FiendTypeId",
                table: "Fiend");

            migrationBuilder.DropTable(
                name: "FiendType");

            migrationBuilder.DropIndex(
                name: "IX_Fiend_FiendTypeId",
                table: "Fiend");

            migrationBuilder.DropColumn(
                name: "FiendTypeId",
                table: "Fiend");
        }
    }
}
