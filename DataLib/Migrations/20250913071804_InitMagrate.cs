using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLib.Migrations
{
    public partial class InitMagrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    DateActivate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateFinish = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Area = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalAccountResident",
                columns: table => new
                {
                    PersonalAccountsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ResidentsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAccountResident", x => new { x.PersonalAccountsId, x.ResidentsId });
                    table.ForeignKey(
                        name: "FK_PersonalAccountResident_PersonalAccounts_PersonalAccountsId",
                        column: x => x.PersonalAccountsId,
                        principalTable: "PersonalAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalAccountResident_Residents_ResidentsId",
                        column: x => x.ResidentsId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAccountResident_ResidentsId",
                table: "PersonalAccountResident",
                column: "ResidentsId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAccounts_Address",
                table: "PersonalAccounts",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAccounts_Number",
                table: "PersonalAccounts",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Residents_LastName",
                table: "Residents",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_LastName_FirstName_SecondName",
                table: "Residents",
                columns: new[] { "LastName", "FirstName", "SecondName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalAccountResident");

            migrationBuilder.DropTable(
                name: "PersonalAccounts");

            migrationBuilder.DropTable(
                name: "Residents");
        }
    }
}
