using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PoolNumber",
                table: "ResponseItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoolNumber",
                table: "ResponseItems");
        }
    }
}
