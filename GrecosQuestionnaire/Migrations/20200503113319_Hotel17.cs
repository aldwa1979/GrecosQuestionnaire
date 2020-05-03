using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPartner_Hotels_HotelModelId",
                table: "HotelPartner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelPartner",
                table: "HotelPartner");

            migrationBuilder.RenameTable(
                name: "HotelPartner",
                newName: "HotelPartners");

            migrationBuilder.RenameIndex(
                name: "IX_HotelPartner_HotelModelId",
                table: "HotelPartners",
                newName: "IX_HotelPartners_HotelModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelPartners",
                table: "HotelPartners",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPartners_Hotels_HotelModelId",
                table: "HotelPartners",
                column: "HotelModelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPartners_Hotels_HotelModelId",
                table: "HotelPartners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelPartners",
                table: "HotelPartners");

            migrationBuilder.RenameTable(
                name: "HotelPartners",
                newName: "HotelPartner");

            migrationBuilder.RenameIndex(
                name: "IX_HotelPartners_HotelModelId",
                table: "HotelPartner",
                newName: "IX_HotelPartner_HotelModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelPartner",
                table: "HotelPartner",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPartner_Hotels_HotelModelId",
                table: "HotelPartner",
                column: "HotelModelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
