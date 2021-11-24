using Microsoft.EntityFrameworkCore.Migrations;

namespace Discord_Bot.Migrations
{
    public partial class FightEventToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FightEvent_Actor_ActorId",
                table: "FightEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_FightEvent_Actor_TargetId",
                table: "FightEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_FightEvent_Fight_FightId",
                table: "FightEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FightEvent",
                table: "FightEvent");

            migrationBuilder.RenameTable(
                name: "FightEvent",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_FightEvent_TargetId",
                table: "Event",
                newName: "IX_Event_TargetId");

            migrationBuilder.RenameIndex(
                name: "IX_FightEvent_FightId",
                table: "Event",
                newName: "IX_Event_FightId");

            migrationBuilder.RenameIndex(
                name: "IX_FightEvent_ActorId",
                table: "Event",
                newName: "IX_Event_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Fight_FightId",
                table: "Event",
                column: "FightId",
                principalTable: "Fight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Actor_ActorId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Actor_TargetId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Fight_FightId",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "FightEvent");

            migrationBuilder.RenameIndex(
                name: "IX_Event_TargetId",
                table: "FightEvent",
                newName: "IX_FightEvent_TargetId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_FightId",
                table: "FightEvent",
                newName: "IX_FightEvent_FightId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ActorId",
                table: "FightEvent",
                newName: "IX_FightEvent_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FightEvent",
                table: "FightEvent",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_FightEvent_Fight_FightId",
                table: "FightEvent",
                column: "FightId",
                principalTable: "Fight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
