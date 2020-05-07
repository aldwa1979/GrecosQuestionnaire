using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel25 : Migration
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
                    PartnerModelID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPartnerModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserPartnerModel_Partners_PartnerModelID",
                        column: x => x.PartnerModelID,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPartnerModel_PartnerModelID",
                table: "UserPartnerModel",
                column: "PartnerModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPartnerModel");
        }
    }
}
