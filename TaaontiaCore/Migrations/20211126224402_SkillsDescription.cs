using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class SkillsDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Skill",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Skill");
        }
    }
}
