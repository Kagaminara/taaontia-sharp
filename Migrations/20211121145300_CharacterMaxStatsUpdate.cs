using Microsoft.EntityFrameworkCore.Migrations;

namespace Discord_Bot.Migrations
{
    public partial class CharacterMaxStatsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mana",
                table: "Character",
                newName: "MaxHealth");

            migrationBuilder.AddColumn<int>(
                name: "Energy",
                table: "Character",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxEnergy",
                table: "Character",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Energy",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "MaxEnergy",
                table: "Character");

            migrationBuilder.RenameColumn(
                name: "MaxHealth",
                table: "Character",
                newName: "Mana");
        }
    }
}
