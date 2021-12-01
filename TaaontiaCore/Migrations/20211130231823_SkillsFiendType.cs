using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class SkillsFiendType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "FiendTypeSkill",
                columns: table => new
                {
                    FiendTypesId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    SkillsId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiendTypeSkill", x => new { x.FiendTypesId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_FiendTypeSkill_FiendType_FiendTypesId",
                        column: x => x.FiendTypesId,
                        principalTable: "FiendType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiendTypeSkill_Skill_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiendTypeSkill_SkillsId",
                table: "FiendTypeSkill",
                column: "SkillsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiendTypeSkill");

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
    }
}
