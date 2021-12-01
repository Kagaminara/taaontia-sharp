using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class FiendTypeSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "FiendTypeId",
                table: "Skill",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_FiendTypeId",
                table: "Skill",
                column: "FiendTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_FiendType_FiendTypeId",
                table: "Skill",
                column: "FiendTypeId",
                principalTable: "FiendType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_FiendType_FiendTypeId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_FiendTypeId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "FiendTypeId",
                table: "Skill");
        }
    }
}
