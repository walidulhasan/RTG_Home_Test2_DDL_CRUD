using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBook.Migrations
{
    public partial class adds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    countryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.countryId);
                });

            migrationBuilder.CreateTable(
                name: "divisions",
                columns: table => new
                {
                    divisionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    divisionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_divisions", x => x.divisionId);
                    table.ForeignKey(
                        name: "FK_divisions_countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "countries",
                        principalColumn: "countryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    districtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    districtName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DivisionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.districtId);
                    table.ForeignKey(
                        name: "FK_districts_divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "divisions",
                        principalColumn: "divisionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    personId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    personPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    personPicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    village = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    countryId = table.Column<int>(type: "int", nullable: false),
                    divisionId = table.Column<int>(type: "int", nullable: false),
                    districtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.personId);
                    table.ForeignKey(
                        name: "FK_persons_countries_countryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "countryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_persons_districts_districtId",
                        column: x => x.districtId,
                        principalTable: "districts",
                        principalColumn: "districtId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_persons_divisions_divisionId",
                        column: x => x.divisionId,
                        principalTable: "divisions",
                        principalColumn: "divisionId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_districts_DivisionId",
                table: "districts",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_divisions_CountryId",
                table: "divisions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_persons_countryId",
                table: "persons",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_persons_districtId",
                table: "persons",
                column: "districtId");

            migrationBuilder.CreateIndex(
                name: "IX_persons_divisionId",
                table: "persons",
                column: "divisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "divisions");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
