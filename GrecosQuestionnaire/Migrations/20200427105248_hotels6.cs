using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class hotels6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "MainRoom");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MainRoom");

            migrationBuilder.AddColumn<string>(
                name: "MainRoomCode",
                table: "MainRoom",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainRoomName",
                table: "MainRoom",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SharedUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SharedRoomCode = table.Column<string>(nullable: true),
                    MainRoomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedUnit_MainRoom_MainRoomId",
                        column: x => x.MainRoomId,
                        principalTable: "MainRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedUnit_MainRoomId",
                table: "SharedUnit",
                column: "MainRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedUnit");

            migrationBuilder.DropColumn(
                name: "MainRoomCode",
                table: "MainRoom");

            migrationBuilder.DropColumn(
                name: "MainRoomName",
                table: "MainRoom");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "MainRoom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MainRoom",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
