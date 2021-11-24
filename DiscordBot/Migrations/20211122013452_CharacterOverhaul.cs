using Microsoft.EntityFrameworkCore.Migrations;

namespace Discord_Bot.Migrations
{
    public partial class CharacterOverhaul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_FiendType_FiendTypeId",
                table: "Actor");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Actor_ActorId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Actor_TargetId",
                table: "Event");

            migrationBuilder.DropTable(
                name: "CharacterFight");

            migrationBuilder.DropTable(
                name: "FiendFight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "Character_Level",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "DiscordDiscriminator",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "DiscordId",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "Energy",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "Health",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "MaxEnergy",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Actor");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Fiend");

            migrationBuilder.RenameColumn(
                name: "ActorId",
                table: "Event",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ActorId",
                table: "Event",
                newName: "IX_Event_AuthorId");

            migrationBuilder.RenameColumn(
                name: "MaxHealth",
                table: "Fiend",
                newName: "CharacterForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_Actor_FiendTypeId",
                table: "Fiend",
                newName: "IX_Fiend_FiendTypeId");

            migrationBuilder.AddColumn<long>(
                name: "FiendId",
                table: "Fight",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlayerId",
                table: "Fight",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fiend",
                table: "Fiend",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Health = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    Energy = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxEnergy = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false),
                    FightId = table.Column<long>(type: "INTEGER", nullable: true),
                    FightId1 = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Fight_FightId",
                        column: x => x.FightId,
                        principalTable: "Fight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_Fight_FightId1",
                        column: x => x.FightId1,
                        principalTable: "Fight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiscordId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    DiscordDiscriminator = table.Column<string>(type: "TEXT", nullable: true),
                    CharacterForeignKey = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Character_CharacterForeignKey",
                        column: x => x.CharacterForeignKey,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fight_FiendId",
                table: "Fight",
                column: "FiendId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_PlayerId",
                table: "Fight",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Fiend_CharacterForeignKey",
                table: "Fiend",
                column: "CharacterForeignKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_FightId",
                table: "Character",
                column: "FightId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_FightId1",
                table: "Character",
                column: "FightId1");

            migrationBuilder.CreateIndex(
                name: "IX_Player_CharacterForeignKey",
                table: "Player",
                column: "CharacterForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Character_AuthorId",
                table: "Event",
                column: "AuthorId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Character_TargetId",
                table: "Event",
                column: "TargetId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fiend_Character_CharacterForeignKey",
                table: "Fiend",
                column: "CharacterForeignKey",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fiend_FiendType_FiendTypeId",
                table: "Fiend",
                column: "FiendTypeId",
                principalTable: "FiendType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fight_Fiend_FiendId",
                table: "Fight",
                column: "FiendId",
                principalTable: "Fiend",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fight_Player_PlayerId",
                table: "Fight",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Character_AuthorId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Character_TargetId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Fiend_Character_CharacterForeignKey",
                table: "Fiend");

            migrationBuilder.DropForeignKey(
                name: "FK_Fiend_FiendType_FiendTypeId",
                table: "Fiend");

            migrationBuilder.DropForeignKey(
                name: "FK_Fight_Fiend_FiendId",
                table: "Fight");

            migrationBuilder.DropForeignKey(
                name: "FK_Fight_Player_PlayerId",
                table: "Fight");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Fight_FiendId",
                table: "Fight");

            migrationBuilder.DropIndex(
                name: "IX_Fight_PlayerId",
                table: "Fight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fiend",
                table: "Fiend");

            migrationBuilder.DropIndex(
                name: "IX_Fiend_CharacterForeignKey",
                table: "Fiend");

            migrationBuilder.DropColumn(
                name: "FiendId",
                table: "Fight");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Fight");

            migrationBuilder.RenameTable(
                name: "Fiend",
                newName: "Actor");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Event",
                newName: "ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_AuthorId",
                table: "Event",
                newName: "IX_Event_ActorId");

            migrationBuilder.RenameColumn(
                name: "CharacterForeignKey",
                table: "Actor",
                newName: "MaxHealth");

            migrationBuilder.RenameIndex(
                name: "IX_Fiend_FiendTypeId",
                table: "Actor",
                newName: "IX_Actor_FiendTypeId");

            migrationBuilder.AddColumn<int>(
                name: "Character_Level",
                table: "Actor",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscordDiscriminator",
                table: "Actor",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "DiscordId",
                table: "Actor",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Actor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Energy",
                table: "Actor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Actor",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Health",
                table: "Actor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Actor",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxEnergy",
                table: "Actor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Actor",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "Id");

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
                        name: "FK_CharacterFight_Actor_AlliesId",
                        column: x => x.AlliesId,
                        principalTable: "Actor",
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
                        name: "FK_FiendFight_Actor_FiendsId",
                        column: x => x.FiendsId,
                        principalTable: "Actor",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_FiendType_FiendTypeId",
                table: "Actor",
                column: "FiendTypeId",
                principalTable: "FiendType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Actor_ActorId",
                table: "Event",
                column: "ActorId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Actor_TargetId",
                table: "Event",
                column: "TargetId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
