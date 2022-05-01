using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDRental.Migrations
{
    public partial class Updated20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberDOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MembershipCategoryNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberNumber);
                    table.ForeignKey(
                        name: "FK_Members_MembershipCategories_MembershipCategoryNumber",
                        column: x => x.MembershipCategoryNumber,
                        principalTable: "MembershipCategories",
                        principalColumn: "MembershipCategoryNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_MemberNumber",
                table: "Loans",
                column: "MemberNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Members_MembershipCategoryNumber",
                table: "Members",
                column: "MembershipCategoryNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberNumber",
                table: "Loans",
                column: "MemberNumber",
                principalTable: "Members",
                principalColumn: "MemberNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberNumber",
                table: "Loans");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Loans_MemberNumber",
                table: "Loans");
        }
    }
}
