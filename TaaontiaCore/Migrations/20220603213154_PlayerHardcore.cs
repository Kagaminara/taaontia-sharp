using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class PlayerHardcore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHardcore",
                table: "Player",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHardcore",
                table: "Player");
        }
    }
}
