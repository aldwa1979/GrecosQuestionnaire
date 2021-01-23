using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hoteld38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionItemItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionItemId = table.Column<int>(nullable: true),
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
                    table.PrimaryKey("PK_QuestionItemItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionItemItems_QuestionItems_QuestionItemId",
                        column: x => x.QuestionItemId,
                        principalTable: "QuestionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionItemItems_QuestionItemId",
                table: "QuestionItemItems",
                column: "QuestionItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionItemItems");
        }
    }
}
