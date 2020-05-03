using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelPartners_HotelPartnerId1",
                table: "Hotels");

            migrationBuilder.DropTable(
                name: "HotelPartners");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelPartnerId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "HotelPartnerId1",
                table: "Hotels");

            migrationBuilder.AlterColumn<int>(
                name: "HotelPartnerId",
                table: "Hotels",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HotelPartnerId",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelPartnerId1",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HotelPartners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelModelId = table.Column<int>(type: "int", nullable: false),
                    HotelPartnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelPartners", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelPartnerId1",
                table: "Hotels",
                column: "HotelPartnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_HotelPartners_HotelPartnerId1",
                table: "Hotels",
                column: "HotelPartnerId1",
                principalTable: "HotelPartners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
