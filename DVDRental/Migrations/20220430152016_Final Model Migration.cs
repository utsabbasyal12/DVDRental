using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDRental.Migrations
{
    public partial class FinalModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DVDTitles_DVDCategory_DVDCategoryCategoryNumber",
                table: "DVDTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_DVDTitles_Producers_ProducerNumber",
                table: "DVDTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_DVDTitles_Studios_StudioId",
                table: "DVDTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_DVDCopies_DVDCopyCopyNumber",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_DVDCopyCopyNumber",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_DVDTitles_DVDCategoryCategoryNumber",
                table: "DVDTitles");

            migrationBuilder.DropColumn(
                name: "DVDCopyCopyNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DVDCategoryCategoryNumber",
                table: "DVDTitles");

            migrationBuilder.AlterColumn<int>(
                name: "MembershipCategoryDescription",
                table: "MembershipCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CopyNumber",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LoanTypeNumber",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemberNumber",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StudioId",
                table: "DVDTitles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProducerNumber",
                table: "DVDTitles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryNumber",
                table: "DVDTitles",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CopyNumber",
                table: "Loans",
                column: "CopyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitles_CategoryNumber",
                table: "DVDTitles",
                column: "CategoryNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_DVDTitles_DVDCategory_CategoryNumber",
                table: "DVDTitles",
                column: "CategoryNumber",
                principalTable: "DVDCategory",
                principalColumn: "CategoryNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DVDTitles_Producers_ProducerNumber",
                table: "DVDTitles",
                column: "ProducerNumber",
                principalTable: "Producers",
                principalColumn: "ProducerNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DVDTitles_Studios_StudioId",
                table: "DVDTitles",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "StudioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_DVDCopies_CopyNumber",
                table: "Loans",
                column: "CopyNumber",
                principalTable: "DVDCopies",
                principalColumn: "CopyNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DVDTitles_DVDCategory_CategoryNumber",
                table: "DVDTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_DVDTitles_Producers_ProducerNumber",
                table: "DVDTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_DVDTitles_Studios_StudioId",
                table: "DVDTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_DVDCopies_CopyNumber",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_CopyNumber",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_DVDTitles_CategoryNumber",
                table: "DVDTitles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_DVDCategory_TempId",
                table: "DVDCategory");

            migrationBuilder.DropColumn(
                name: "CopyNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LoanTypeNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MemberNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CategoryNumber",
                table: "DVDTitles");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "DVDCategory");

            migrationBuilder.AlterColumn<string>(
                name: "MembershipCategoryDescription",
                table: "MembershipCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DVDCopyCopyNumber",
                table: "Loans",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudioId",
                table: "DVDTitles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProducerNumber",
                table: "DVDTitles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DVDCategoryCategoryNumber",
                table: "DVDTitles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_DVDCopyCopyNumber",
                table: "Loans",
                column: "DVDCopyCopyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitles_DVDCategoryCategoryNumber",
                table: "DVDTitles",
                column: "DVDCategoryCategoryNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_DVDTitles_DVDCategory_DVDCategoryCategoryNumber",
                table: "DVDTitles",
                column: "DVDCategoryCategoryNumber",
                principalTable: "DVDCategory",
                principalColumn: "CategoryNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_DVDTitles_Producers_ProducerNumber",
                table: "DVDTitles",
                column: "ProducerNumber",
                principalTable: "Producers",
                principalColumn: "ProducerNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_DVDTitles_Studios_StudioId",
                table: "DVDTitles",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "StudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_DVDCopies_DVDCopyCopyNumber",
                table: "Loans",
                column: "DVDCopyCopyNumber",
                principalTable: "DVDCopies",
                principalColumn: "CopyNumber");
        }
    }
}
