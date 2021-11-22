using Microsoft.EntityFrameworkCore.Migrations;

namespace Discord_Bot.Migrations
{
    public partial class EventRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterFight_Character_AlliesId",
                table: "CharacterFight");

            migrationBuilder.DropForeignKey(
                name: "FK_Fiend_FiendType_FiendTypeId",
                table: "Fiend");

            migrationBuilder.DropForeignKey(
                name: "FK_FiendFight_Fiend_FiendsId",
                table: "FiendFight");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fiend",
                table: "Fiend");

            migrationBuilder.RenameTable(
                name: "Fiend",
                newName: "Actor");

            migrationBuilder.RenameIndex(
                name: "IX_Fiend_FiendTypeId",
                table: "Actor",
                newName: "IX_Actor_FiendTypeId");

            migrationBuilder.AddColumn<long>(
                name: "ActorId",
                table: "FightEvent",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TargetId",
                table: "FightEvent",
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
                name: "Experience",
                table: "Actor",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxEnergy",
                table: "Actor",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxHealth",
                table: "Actor",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FightEvent_ActorId",
                table: "FightEvent",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_FightEvent_TargetId",
                table: "FightEvent",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_FiendType_FiendTypeId",
                table: "Actor",
                column: "FiendTypeId",
                principalTable: "FiendType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterFight_Actor_AlliesId",
                table: "CharacterFight",
                column: "AlliesId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FiendFight_Actor_FiendsId",
                table: "FiendFight",
                column: "FiendsId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FightEvent_Actor_ActorId",
                table: "FightEvent",
                column: "ActorId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FightEvent_Actor_TargetId",
                table: "FightEvent",
                column: "TargetId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_FiendType_FiendTypeId",
                table: "Actor");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterFight_Actor_AlliesId",
                table: "CharacterFight");

            migrationBuilder.DropForeignKey(
                name: "FK_FiendFight_Actor_FiendsId",
                table: "FiendFight");

            migrationBuilder.DropForeignKey(
                name: "FK_FightEvent_Actor_ActorId",
                table: "FightEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_FightEvent_Actor_TargetId",
                table: "FightEvent");

            migrationBuilder.DropIndex(
                name: "IX_FightEvent_ActorId",
                table: "FightEvent");

            migrationBuilder.DropIndex(
                name: "IX_FightEvent_TargetId",
                table: "FightEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "FightEvent");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "FightEvent");

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
                name: "Experience",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "MaxEnergy",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "MaxHealth",
                table: "Actor");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Fiend");

            migrationBuilder.RenameIndex(
                name: "IX_Actor_FiendTypeId",
                table: "Fiend",
                newName: "IX_Fiend_FiendTypeId");

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
                    DiscordDiscriminator = table.Column<string>(type: "TEXT", nullable: true),
                    DiscordId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Energy = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false),
                    Health = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxEnergy = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterFight_Character_AlliesId",
                table: "CharacterFight",
                column: "AlliesId",
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
                name: "FK_FiendFight_Fiend_FiendsId",
                table: "FiendFight",
                column: "FiendsId",
                principalTable: "Fiend",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
