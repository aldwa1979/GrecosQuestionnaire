using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainRoom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelModelId = table.Column<int>(type: "int", nullable: true),
                    MainRoomCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainRoomName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainRoom_Hotels_HotelModelId",
                        column: x => x.HotelModelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainRoom_HotelModelId",
                table: "MainRoom",
                column: "HotelModelId");
        }
    }
}
