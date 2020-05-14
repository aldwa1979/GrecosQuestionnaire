using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPartners_Partners_PartnerModelID",
                table: "UsersPartners");

            migrationBuilder.DropIndex(
                name: "IX_UsersPartners_PartnerModelID",
                table: "UsersPartners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersPartners_PartnerModelID",
                table: "UsersPartners",
                column: "PartnerModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersPartners_Partners_PartnerModelID",
                table: "UsersPartners",
                column: "PartnerModelID",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
