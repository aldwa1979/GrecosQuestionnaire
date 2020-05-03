using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainRooms_Hotels_HotelModelId",
                table: "MainRooms");

            migrationBuilder.AlterColumn<int>(
                name: "HotelModelId",
                table: "MainRooms",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MainRooms_Hotels_HotelModelId",
                table: "MainRooms",
                column: "HotelModelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainRooms_Hotels_HotelModelId",
                table: "MainRooms");

            migrationBuilder.AlterColumn<int>(
                name: "HotelModelId",
                table: "MainRooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MainRooms_Hotels_HotelModelId",
                table: "MainRooms",
                column: "HotelModelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
