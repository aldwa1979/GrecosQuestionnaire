using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemPage = table.Column<int>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    IsHeader = table.Column<bool>(nullable: false),
                    Removed = table.Column<bool>(nullable: false),
                    Class = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionItemModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(nullable: true),
                    ItemOrder = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Items = table.Column<string>(nullable: true),
                    QuestionItemType = table.Column<int>(nullable: false),
                    Parts = table.Column<int>(nullable: false),
                    SingleSpace = table.Column<int>(nullable: false),
                    Required = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionItemModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionItemModel_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionItemModel_QuestionId",
                table: "QuestionItemModel",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionItemModel");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
