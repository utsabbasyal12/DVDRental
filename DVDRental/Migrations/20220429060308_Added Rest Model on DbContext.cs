using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDRental.Migrations
{
    public partial class AddedRestModelonDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DVDCopies",
                columns: table => new
                {
                    CopyNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePurchased = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DVDTitleDVDNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDCopies", x => x.CopyNumber);
                    table.ForeignKey(
                        name: "FK_DVDCopies_DVDTitles_DVDTitleDVDNumber",
                        column: x => x.DVDTitleDVDNumber,
                        principalTable: "DVDTitles",
                        principalColumn: "DVDNumber");
                });

            migrationBuilder.CreateTable(
                name: "MembershipCategories",
                columns: table => new
                {
                    MembershipCategoryNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembershipCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipCategoryTotalLoans = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipCategories", x => x.MembershipCategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DVDCopyCopyNumber = table.Column<int>(type: "int", nullable: true),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRetured = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanNumber);
                    table.ForeignKey(
                        name: "FK_Loans_DVDCopies_DVDCopyCopyNumber",
                        column: x => x.DVDCopyCopyNumber,
                        principalTable: "DVDCopies",
                        principalColumn: "CopyNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DVDCopies_DVDTitleDVDNumber",
                table: "DVDCopies",
                column: "DVDTitleDVDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_DVDCopyCopyNumber",
                table: "Loans",
                column: "DVDCopyCopyNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "MembershipCategories");

            migrationBuilder.DropTable(
                name: "DVDCopies");
        }
    }
}
