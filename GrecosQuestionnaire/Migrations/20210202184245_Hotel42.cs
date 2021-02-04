using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseItemItems_QuestionItemItems_QuestionItemItemId",
                table: "ResponseItemItems");

            migrationBuilder.DropIndex(
                name: "IX_ResponseItemItems_QuestionItemItemId",
                table: "ResponseItemItems");

            migrationBuilder.DropColumn(
                name: "QuestionItemItemId",
                table: "ResponseItemItems");

            migrationBuilder.AddColumn<int>(
                name: "QuestionItemItem",
                table: "ResponseItemItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionItemItem",
                table: "ResponseItemItems");

            migrationBuilder.AddColumn<int>(
                name: "QuestionItemItemId",
                table: "ResponseItemItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResponseItemItems_QuestionItemItemId",
                table: "ResponseItemItems",
                column: "QuestionItemItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseItemItems_QuestionItemItems_QuestionItemItemId",
                table: "ResponseItemItems",
                column: "QuestionItemItemId",
                principalTable: "QuestionItemItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
