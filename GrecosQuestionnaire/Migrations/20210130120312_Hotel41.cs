using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel41 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResponseItemItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionItemItemId = table.Column<int>(nullable: true),
                    ResponseItemId = table.Column<int>(nullable: true),
                    RawValue = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseItemItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponseItemItems_QuestionItemItems_QuestionItemItemId",
                        column: x => x.QuestionItemItemId,
                        principalTable: "QuestionItemItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResponseItemItems_ResponseItems_ResponseItemId",
                        column: x => x.ResponseItemId,
                        principalTable: "ResponseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResponseItemItems_QuestionItemItemId",
                table: "ResponseItemItems",
                column: "QuestionItemItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseItemItems_ResponseItemId",
                table: "ResponseItemItems",
                column: "ResponseItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResponseItemItems");
        }
    }
}
