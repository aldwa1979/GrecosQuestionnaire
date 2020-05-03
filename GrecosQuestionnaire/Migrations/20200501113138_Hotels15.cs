using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotels15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomModelId",
                table: "SharedUnits");

            migrationBuilder.DropColumn(
                name: "MainRoomId",
                table: "SharedUnits");

            migrationBuilder.AlterColumn<int>(
                name: "MainRoomModelId",
                table: "SharedUnits",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomModelId",
                table: "SharedUnits",
                column: "MainRoomModelId",
                principalTable: "MainRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomModelId",
                table: "SharedUnits");

            migrationBuilder.AlterColumn<int>(
                name: "MainRoomModelId",
                table: "SharedUnits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MainRoomId",
                table: "SharedUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomModelId",
                table: "SharedUnits",
                column: "MainRoomModelId",
                principalTable: "MainRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
