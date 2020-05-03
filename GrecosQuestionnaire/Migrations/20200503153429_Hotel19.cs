using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPartners_Hotels_HotelModelId",
                table: "HotelPartners");

            migrationBuilder.DropIndex(
                name: "IX_HotelPartners_HotelModelId",
                table: "HotelPartners");

            migrationBuilder.AddColumn<int>(
                name: "HotelPartnerId",
                table: "Hotels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HotelPartnerId1",
                table: "Hotels",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelPartners_HotelPartnerId1",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelPartnerId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "HotelPartnerId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "HotelPartnerId1",
                table: "Hotels");

            migrationBuilder.CreateIndex(
                name: "IX_HotelPartners_HotelModelId",
                table: "HotelPartners",
                column: "HotelModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPartners_Hotels_HotelModelId",
                table: "HotelPartners",
                column: "HotelModelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
