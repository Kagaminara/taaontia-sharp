using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class SkillsOnCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CharacterId",
                table: "Skill",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_CharacterId",
                table: "Skill",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Character_CharacterId",
                table: "Skill",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Character_CharacterId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_CharacterId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Skill");
        }
    }
}
