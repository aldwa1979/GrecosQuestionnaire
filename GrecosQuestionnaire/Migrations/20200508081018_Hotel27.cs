using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPartnerModel_Partners_PartnerModelID",
                table: "UserPartnerModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPartnerModel",
                table: "UserPartnerModel");

            migrationBuilder.RenameTable(
                name: "UserPartnerModel",
                newName: "UsersPartners");

            migrationBuilder.RenameIndex(
                name: "IX_UserPartnerModel_PartnerModelID",
                table: "UsersPartners",
                newName: "IX_UsersPartners_PartnerModelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersPartners",
                table: "UsersPartners",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersPartners_Partners_PartnerModelID",
                table: "UsersPartners",
                column: "PartnerModelID",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPartners_Partners_PartnerModelID",
                table: "UsersPartners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersPartners",
                table: "UsersPartners");

            migrationBuilder.RenameTable(
                name: "UsersPartners",
                newName: "UserPartnerModel");

            migrationBuilder.RenameIndex(
                name: "IX_UsersPartners_PartnerModelID",
                table: "UserPartnerModel",
                newName: "IX_UserPartnerModel_PartnerModelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPartnerModel",
                table: "UserPartnerModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPartnerModel_Partners_PartnerModelID",
                table: "UserPartnerModel",
                column: "PartnerModelID",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
