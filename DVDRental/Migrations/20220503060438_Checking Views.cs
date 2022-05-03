using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDRental.Migrations
{
    public partial class CheckingViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_DVDCategory_TempId",
                table: "DVDCategory");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "DVDCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "DVDCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DVDCategory_TempId",
                table: "DVDCategory",
                column: "TempId");
        }
    }
}
