using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaxCentre.Server.Migrations
{
    /// <inheritdoc />
    public partial class reciepts3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccineVaccineCentre_vaccines_VaccinesId",
                table: "VaccineVaccineCentre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vaccines",
                table: "vaccines");

            migrationBuilder.RenameTable(
                name: "vaccines",
                newName: "Vaccines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VaccinationReciepts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    VaccineCentreId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VaccineDose1Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Dose1State = table.Column<int>(type: "int", nullable: false),
                    VaccineDose2Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Dose2State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationReciepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccinationReciepts_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccinationReciepts_AspNetUsers_VaccineCentreId",
                        column: x => x.VaccineCentreId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccinationReciepts_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationReciepts_PatientId",
                table: "VaccinationReciepts",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationReciepts_VaccineCentreId",
                table: "VaccinationReciepts",
                column: "VaccineCentreId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationReciepts_VaccineId",
                table: "VaccinationReciepts",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineVaccineCentre_Vaccines_VaccinesId",
                table: "VaccineVaccineCentre",
                column: "VaccinesId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccineVaccineCentre_Vaccines_VaccinesId",
                table: "VaccineVaccineCentre");

            migrationBuilder.DropTable(
                name: "VaccinationReciepts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines");

            migrationBuilder.RenameTable(
                name: "Vaccines",
                newName: "vaccines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vaccines",
                table: "vaccines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineVaccineCentre_vaccines_VaccinesId",
                table: "VaccineVaccineCentre",
                column: "VaccinesId",
                principalTable: "vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
