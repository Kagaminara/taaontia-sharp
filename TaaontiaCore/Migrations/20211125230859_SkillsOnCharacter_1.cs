using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class SkillsOnCharacter_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "StatusTypeId",
                table: "Character",
                type: "INTEGER",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_StatusType_StatusTypeId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_StatusTypeId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "StatusTypeId",
                table: "Character");
        }
    }
}
