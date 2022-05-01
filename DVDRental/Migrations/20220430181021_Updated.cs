using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDRental.Migrations
{
    public partial class Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanTypes",
                columns: table => new
                {
                    LoanTypeNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanCategory = table.Column<int>(type: "int", nullable: false),
                    LoanDuration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTypes", x => x.LoanTypeNumber);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanTypeNumber",
                table: "Loans",
                column: "LoanTypeNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_LoanTypes_LoanTypeNumber",
                table: "Loans",
                column: "LoanTypeNumber",
                principalTable: "LoanTypes",
                principalColumn: "LoanTypeNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanTypes_LoanTypeNumber",
                table: "Loans");

            migrationBuilder.DropTable(
                name: "LoanTypes");

            migrationBuilder.DropIndex(
                name: "IX_Loans_LoanTypeNumber",
                table: "Loans");
        }
    }
}
