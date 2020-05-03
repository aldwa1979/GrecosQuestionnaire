using Microsoft.EntityFrameworkCore.Migrations;

namespace GrecosQuestionnaire.Migrations
{
    public partial class Hotel18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPartners_Hotels_HotelModelId",
                table: "HotelPartners");

            migrationBuilder.AlterColumn<int>(
                name: "HotelModelId",
                table: "HotelPartners",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPartners_Hotels_HotelModelId",
                table: "HotelPartners",
                column: "HotelModelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPartners_Hotels_HotelModelId",
                table: "HotelPartners");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.AlterColumn<int>(
                name: "HotelModelId",
                table: "HotelPartners",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPartners_Hotels_HotelModelId",
                table: "HotelPartners",
                column: "HotelModelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
