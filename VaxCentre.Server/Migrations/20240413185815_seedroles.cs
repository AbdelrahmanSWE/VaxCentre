using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VaxCentre.Server.Migrations
{
    /// <inheritdoc />
    public partial class seedroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9aa621ac-55e9-4fd6-b84d-284fbb363e12", null, "Admin", "ADMIN" },
                    { "d5f31437-3ba5-4bf3-b6bf-3b99618a47d5", null, "Patient", "PATIENT" },
                    { "e614ef58-51a4-4e25-a3e3-e8bdc48ac2ec", null, "VaccineCenter", "VACCINECENTER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aa621ac-55e9-4fd6-b84d-284fbb363e12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5f31437-3ba5-4bf3-b6bf-3b99618a47d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e614ef58-51a4-4e25-a3e3-e8bdc48ac2ec");
        }
    }
}
