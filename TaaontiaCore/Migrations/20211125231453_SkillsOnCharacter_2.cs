using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class SkillsOnCharacter_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_StatusType_StatusTypeId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Character_CharacterId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_CharacterId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Character_StatusTypeId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "StatusTypeId",
                table: "Character");

            migrationBuilder.CreateTable(
                name: "CharacterSkill",
                columns: table => new
                {
                    CharactersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SkillsId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkill", x => new { x.CharactersId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_CharacterSkill_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSkill_Skill_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkill_SkillsId",
                table: "CharacterSkill",
                column: "SkillsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkill");

            migrationBuilder.AddColumn<Guid>(
                name: "CharacterId",
                table: "Skill",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "StatusTypeId",
                table: "Character",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_CharacterId",
                table: "Skill",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_StatusTypeId",
                table: "Character",
                column: "StatusTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_StatusType_StatusTypeId",
                table: "Character",
                column: "StatusTypeId",
                principalTable: "StatusType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Character_CharacterId",
                table: "Skill",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
