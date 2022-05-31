using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fiend",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Health = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    Energy = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxEnergy = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fiend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FiendType",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
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
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RemoteId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Health = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    Energy = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxEnergy = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusType",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Effect = table.Column<int>(type: "INTEGER", nullable: true),
                    BaseValue = table.Column<int>(type: "INTEGER", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fight",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FiendId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsGlobal = table.Column<bool>(type: "INTEGER", nullable: false)
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
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    BaseSourceDamage = table.Column<int>(type: "INTEGER", nullable: true),
                    BaseTargetDamage = table.Column<int>(type: "INTEGER", nullable: true),
                    SourceStatusId = table.Column<uint>(type: "INTEGER", nullable: true),
                    TargetStatusId = table.Column<uint>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_StatusType_SourceStatusId",
                        column: x => x.SourceStatusId,
                        principalTable: "StatusType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Skill_StatusType_TargetStatusId",
                        column: x => x.TargetStatusId,
                        principalTable: "StatusType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StatusTypeId = table.Column<uint>(type: "INTEGER", nullable: true),
                    RemainingDuration = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_StatusType_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalTable: "StatusType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FightId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TargetId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Fiend_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Fiend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Fight_FightId",
                        column: x => x.FightId,
                        principalTable: "Fight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiendSkill",
                columns: table => new
                {
                    FiendsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SkillsId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiendSkill", x => new { x.FiendsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_FiendSkill_Fiend_FiendsId",
                        column: x => x.FiendsId,
                        principalTable: "Fiend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiendSkill_Skill_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "PlayerSkill",
                columns: table => new
                {
                    PlayersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SkillsId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSkill", x => new { x.PlayersId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_PlayerSkill_Player_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSkill_Skill_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_FightId",
                table: "Event",
                column: "FightId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_PlayerId",
                table: "Event",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_TargetId",
                table: "Event",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_FiendSkill_SkillsId",
                table: "FiendSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_FiendTypeSkill_SkillsId",
                table: "FiendTypeSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_FiendId",
                table: "Fight",
                column: "FiendId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_PlayerId",
                table: "Fight",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSkill_SkillsId",
                table: "PlayerSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_SourceStatusId",
                table: "Skill",
                column: "SourceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_TargetStatusId",
                table: "Skill",
                column: "TargetStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_StatusTypeId",
                table: "Status",
                column: "StatusTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "FiendSkill");

            migrationBuilder.DropTable(
                name: "FiendTypeSkill");

            migrationBuilder.DropTable(
                name: "PlayerSkill");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Fight");

            migrationBuilder.DropTable(
                name: "FiendType");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Fiend");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "StatusType");
        }
    }
}
