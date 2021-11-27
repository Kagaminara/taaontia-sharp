using Microsoft.EntityFrameworkCore.Migrations;

namespace TaaontiaCore.Migrations
{
    public partial class PlayerRemoteId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "RemoteId",
                table: "Player",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0ul);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemoteId",
                table: "Player");
        }
    }
}
