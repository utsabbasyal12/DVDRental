using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDRental.Migrations
{
    public partial class AddedDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Studios",
                newName: "StudioId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Actors",
                newName: "ActorId");

            migrationBuilder.CreateTable(
                name: "DVDCategory",
                columns: table => new
                {
                    CategoryNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeRestricted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDCategory", x => x.CategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    ProducerNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.ProducerNumber);
                });

            migrationBuilder.CreateTable(
                name: "DVDTitles",
                columns: table => new
                {
                    DVDNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRelease = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StandardCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PenaltyCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DVDCategoryCategoryNumber = table.Column<int>(type: "int", nullable: true),
                    StudioId = table.Column<int>(type: "int", nullable: true),
                    ProducerNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDTitles", x => x.DVDNumber);
                    table.ForeignKey(
                        name: "FK_DVDTitles_DVDCategory_DVDCategoryCategoryNumber",
                        column: x => x.DVDCategoryCategoryNumber,
                        principalTable: "DVDCategory",
                        principalColumn: "CategoryNumber");
                    table.ForeignKey(
                        name: "FK_DVDTitles_Producers_ProducerNumber",
                        column: x => x.ProducerNumber,
                        principalTable: "Producers",
                        principalColumn: "ProducerNumber");
                    table.ForeignKey(
                        name: "FK_DVDTitles_Studios_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studios",
                        principalColumn: "StudioId");
                });

            migrationBuilder.CreateTable(
                name: "CastMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    DVDNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CastMembers_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "ActorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastMembers_DVDTitles_DVDNumber",
                        column: x => x.DVDNumber,
                        principalTable: "DVDTitles",
                        principalColumn: "DVDNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastMembers_ActorId",
                table: "CastMembers",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_CastMembers_DVDNumber",
                table: "CastMembers",
                column: "DVDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitles_DVDCategoryCategoryNumber",
                table: "DVDTitles",
                column: "DVDCategoryCategoryNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitles_ProducerNumber",
                table: "DVDTitles",
                column: "ProducerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitles_StudioId",
                table: "DVDTitles",
                column: "StudioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastMembers");

            migrationBuilder.DropTable(
                name: "DVDTitles");

            migrationBuilder.DropTable(
                name: "DVDCategory");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.RenameColumn(
                name: "StudioId",
                table: "Studios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ActorId",
                table: "Actors",
                newName: "Id");
        }
    }
}
