using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaxCentre.Server.Migrations
{
    /// <inheritdoc />
    public partial class reciepts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccineVaccineCentre_vaccines_VaccinesID",
                table: "VaccineVaccineCentre");

            migrationBuilder.DropColumn(
                name: "PrivilegeLevel",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "VaccinesID",
                table: "VaccineVaccineCentre",
                newName: "VaccinesId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineVaccineCentre_VaccinesID",
                table: "VaccineVaccineCentre",
                newName: "IX_VaccineVaccineCentre_VaccinesId");

            migrationBuilder.RenameColumn(
                name: "precaution",
                table: "vaccines",
                newName: "Precaution");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "vaccines",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "gapTime",
                table: "vaccines",
                newName: "GapTime");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "vaccines",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "vaccines",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineVaccineCentre_vaccines_VaccinesId",
                table: "VaccineVaccineCentre",
                column: "VaccinesId",
                principalTable: "vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccineVaccineCentre_vaccines_VaccinesId",
                table: "VaccineVaccineCentre");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "VaccinesId",
                table: "VaccineVaccineCentre",
                newName: "VaccinesID");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineVaccineCentre_VaccinesId",
                table: "VaccineVaccineCentre",
                newName: "IX_VaccineVaccineCentre_VaccinesID");

            migrationBuilder.RenameColumn(
                name: "Precaution",
                table: "vaccines",
                newName: "precaution");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "vaccines",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "GapTime",
                table: "vaccines",
                newName: "gapTime");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "vaccines",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "vaccines",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "PrivilegeLevel",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineVaccineCentre_vaccines_VaccinesID",
                table: "VaccineVaccineCentre",
                column: "VaccinesID",
                principalTable: "vaccines",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
