using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseItems_Responses_ResponseIdId",
                table: "ResponseItems");

            migrationBuilder.DropIndex(
                name: "IX_ResponseItems_ResponseIdId",
                table: "ResponseItems");

            migrationBuilder.DropColumn(
                name: "PoolNumber",
                table: "ResponseItems");

            migrationBuilder.DropColumn(
                name: "ResponseIdId",
                table: "ResponseItems");

            migrationBuilder.AddColumn<int>(
                name: "ResponseId",
                table: "ResponseItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResponseItems_ResponseId",
                table: "ResponseItems",
                column: "ResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseItems_Responses_ResponseId",
                table: "ResponseItems",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseItems_Responses_ResponseId",
                table: "ResponseItems");

            migrationBuilder.DropIndex(
                name: "IX_ResponseItems_ResponseId",
                table: "ResponseItems");

            migrationBuilder.DropColumn(
                name: "ResponseId",
                table: "ResponseItems");

            migrationBuilder.AddColumn<int>(
                name: "PoolNumber",
                table: "ResponseItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResponseIdId",
                table: "ResponseItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResponseItems_ResponseIdId",
                table: "ResponseItems",
                column: "ResponseIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseItems_Responses_ResponseIdId",
                table: "ResponseItems",
                column: "ResponseIdId",
                principalTable: "Responses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
