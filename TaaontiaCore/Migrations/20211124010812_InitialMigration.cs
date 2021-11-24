﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EightBallAnswer",
                columns: table => new
                {
                    AnswerId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnswerText = table.Column<string>(type: "TEXT", nullable: true),
                    AnswerColor = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EightBallAnswer", x => x.AnswerId);
                });

            migrationBuilder.CreateTable(
                name: "FiendType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    BaseHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseEnergy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiendType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FightId = table.Column<long>(type: "INTEGER", nullable: true),
                    AuthorId = table.Column<long>(type: "INTEGER", nullable: true),
                    TargetId = table.Column<long>(type: "INTEGER", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fiend",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterForeignKey = table.Column<long>(type: "INTEGER", nullable: false),
                    FiendTypeId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fiend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fiend_FiendType_FiendTypeId",
                        column: x => x.FiendTypeId,
                        principalTable: "FiendType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterForeignKey = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fight",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsGlobal = table.Column<bool>(type: "INTEGER", nullable: false),
                    FiendId = table.Column<long>(type: "INTEGER", nullable: true),
                    PlayerId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fight_Fiend_FiendId",
                        column: x => x.FiendId,
                        principalTable: "Fiend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fight_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Character_FightId",
                table: "Character",
                column: "FightId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_FightId1",
                table: "Character",
                column: "FightId1");

            migrationBuilder.CreateIndex(
                name: "IX_Event_AuthorId",
                table: "Event",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_FightId",
                table: "Event",
                column: "FightId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_TargetId",
                table: "Event",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Fiend_CharacterForeignKey",
                table: "Fiend",
                column: "CharacterForeignKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fiend_FiendTypeId",
                table: "Fiend",
                column: "FiendTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_FiendId",
                table: "Fight",
                column: "FiendId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_PlayerId",
                table: "Fight",
                column: "PlayerId");

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
                name: "FK_Event_Fight_FightId",
                table: "Event",
                column: "FightId",
                principalTable: "Fight",
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
                name: "FK_Player_Character_CharacterForeignKey",
                table: "Player",
                column: "CharacterForeignKey",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Fight_FightId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Fight_FightId1",
                table: "Character");

            migrationBuilder.DropTable(
                name: "EightBallAnswer");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Fight");

            migrationBuilder.DropTable(
                name: "Fiend");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "FiendType");

            migrationBuilder.DropTable(
                name: "Character");
        }
    }
}
