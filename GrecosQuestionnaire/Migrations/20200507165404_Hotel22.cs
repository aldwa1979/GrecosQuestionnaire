using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPartnerModel",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    PartnerID = table.Column<string>(nullable: true),
                    PartnerModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPartnerModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserPartnerModel_Partners_PartnerModelId",
                        column: x => x.PartnerModelId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPartnerModel_PartnerModelId",
                table: "UserPartnerModel",
                column: "PartnerModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPartnerModel");
        }
    }
}
