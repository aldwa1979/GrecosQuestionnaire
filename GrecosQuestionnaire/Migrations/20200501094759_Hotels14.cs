using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotels14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomId",
                table: "SharedUnits");

            migrationBuilder.DropIndex(
                name: "IX_SharedUnits_MainRoomId",
                table: "SharedUnits");

            migrationBuilder.DropColumn(
                name: "DestinationSeasonName",
                table: "Hotels");

            migrationBuilder.AddColumn<int>(
                name: "MainRoomModelId",
                table: "SharedUnits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Season",
                table: "Hotels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Season = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedUnits_MainRoomModelId",
                table: "SharedUnits",
                column: "MainRoomModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomModelId",
                table: "SharedUnits",
                column: "MainRoomModelId",
                principalTable: "MainRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomModelId",
                table: "SharedUnits");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_SharedUnits_MainRoomModelId",
                table: "SharedUnits");

            migrationBuilder.DropColumn(
                name: "MainRoomModelId",
                table: "SharedUnits");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Hotels");

            migrationBuilder.AddColumn<string>(
                name: "DestinationSeasonName",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedUnits_MainRoomId",
                table: "SharedUnits",
                column: "MainRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedUnits_MainRooms_MainRoomId",
                table: "SharedUnits",
                column: "MainRoomId",
                principalTable: "MainRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
