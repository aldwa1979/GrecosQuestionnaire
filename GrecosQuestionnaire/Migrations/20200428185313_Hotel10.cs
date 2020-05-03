using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainRoom_Hotels_HotelModelId",
                table: "MainRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainRoom",
                table: "MainRoom");

            migrationBuilder.RenameTable(
                name: "MainRoom",
                newName: "MainRooms");

            migrationBuilder.RenameIndex(
                name: "IX_MainRoom_HotelModelId",
                table: "MainRooms",
                newName: "IX_MainRooms_HotelModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainRooms",
                table: "MainRooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MainRooms_Hotels_HotelModelId",
                table: "MainRooms",
                column: "HotelModelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainRooms_Hotels_HotelModelId",
                table: "MainRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainRooms",
                table: "MainRooms");

            migrationBuilder.RenameTable(
                name: "MainRooms",
                newName: "MainRoom");

            migrationBuilder.RenameIndex(
                name: "IX_MainRooms_HotelModelId",
                table: "MainRoom",
                newName: "IX_MainRoom_HotelModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainRoom",
                table: "MainRoom",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MainRoom_Hotels_HotelModelId",
                table: "MainRoom",
                column: "HotelModelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
