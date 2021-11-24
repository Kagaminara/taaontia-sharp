using Microsoft.EntityFrameworkCore.Migrations;

namespace Discord_Bot.Migrations
{
    public partial class FightAndCharacter_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Fight_FightId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Fiend_Fight_FightId",
                table: "Fiend");

            migrationBuilder.DropIndex(
                name: "IX_Fiend_FightId",
                table: "Fiend");

            migrationBuilder.DropIndex(
                name: "IX_Character_FightId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "FightId",
                table: "Fiend");

            migrationBuilder.DropColumn(
                name: "FightId",
                table: "Character");

            migrationBuilder.CreateTable(
                name: "CharacterFight",
                columns: table => new
                {
                    AlliesId = table.Column<long>(type: "INTEGER", nullable: false),
                    FightsId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFight", x => new { x.AlliesId, x.FightsId });
                    table.ForeignKey(
                        name: "FK_CharacterFight_Character_AlliesId",
                        column: x => x.AlliesId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterFight_Fight_FightsId",
                        column: x => x.FightsId,
                        principalTable: "Fight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiendFight",
                columns: table => new
                {
                    FiendsId = table.Column<long>(type: "INTEGER", nullable: false),
                    FightsId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiendFight", x => new { x.FiendsId, x.FightsId });
                    table.ForeignKey(
                        name: "FK_FiendFight_Fiend_FiendsId",
                        column: x => x.FiendsId,
                        principalTable: "Fiend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiendFight_Fight_FightsId",
                        column: x => x.FightsId,
                        principalTable: "Fight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFight_FightsId",
                table: "CharacterFight",
                column: "FightsId");

            migrationBuilder.CreateIndex(
                name: "IX_FiendFight_FightsId",
                table: "FiendFight",
                column: "FightsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterFight");

            migrationBuilder.DropTable(
                name: "FiendFight");

            migrationBuilder.AddColumn<long>(
                name: "FightId",
                table: "Fiend",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FightId",
                table: "Character",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fiend_FightId",
                table: "Fiend",
                column: "FightId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_FightId",
                table: "Character",
                column: "FightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Fight_FightId",
                table: "Character",
                column: "FightId",
                principalTable: "Fight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fiend_Fight_FightId",
                table: "Fiend",
                column: "FightId",
                principalTable: "Fight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
