using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotels13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedUnit_MainRooms_MainRoomId",
                table: "SharedUnit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedUnit",
                table: "SharedUnit");

            migrationBuilder.DropColumn(
                name: "MainRoomName",
                table: "MainRooms");

            migrationBuilder.RenameTable(
                name: "SharedUnit",
                newName: "SharedUnits");

            migrationBuilder.RenameIndex(
                name: "IX_SharedUnit_MainRoomId",
                table: "SharedUnits",
                newName: "IX_SharedUnits_MainRoomId");

            migrationBuilder.AddColumn<string>(
                name: "SharedRoomName",
                table: "SharedUnits",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedUnits",
                table: "SharedUnits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomId",
                table: "SharedUnits",
                column: "MainRoomId",
                principalTable: "MainRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomId",
                table: "SharedUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedUnits",
                table: "SharedUnits");

            migrationBuilder.DropColumn(
                name: "SharedRoomName",
                table: "SharedUnits");

            migrationBuilder.RenameTable(
                name: "SharedUnits",
                newName: "SharedUnit");

            migrationBuilder.RenameIndex(
                name: "IX_SharedUnits_MainRoomId",
                table: "SharedUnit",
                newName: "IX_SharedUnit_MainRoomId");

            migrationBuilder.AddColumn<string>(
                name: "MainRoomName",
                table: "MainRooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedUnit",
                table: "SharedUnit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedUnit_MainRooms_MainRoomId",
                table: "SharedUnit",
                column: "MainRoomId",
                principalTable: "MainRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
