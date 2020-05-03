using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class hotels4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomAllocCode",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "RoomCode",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "RoomDesc",
                table: "Hotels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomAllocCode",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomCode",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomDesc",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
