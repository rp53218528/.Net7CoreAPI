using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Learning.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00d3064f-d10d-4413-a975-01260b023085"), "Easy" },
                    { new Guid("1778979e-939d-49b8-b286-86eb14455663"), "Medium" },
                    { new Guid("3652039f-0098-430b-8ed0-7235b742e527"), "hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2a87cb1a-33d9-469b-9731-c029fe01d622"), "AKL", "Auckland", "" },
                    { new Guid("8af7070f-6704-49f6-8830-c58275a124f0"), "TEST", "TESTING", "" },
                    { new Guid("f4e96d60-fbd5-420f-9845-820edcf66815"), "HP", "Harsh", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("00d3064f-d10d-4413-a975-01260b023085"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1778979e-939d-49b8-b286-86eb14455663"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3652039f-0098-430b-8ed0-7235b742e527"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2a87cb1a-33d9-469b-9731-c029fe01d622"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8af7070f-6704-49f6-8830-c58275a124f0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f4e96d60-fbd5-420f-9845-820edcf66815"));
        }
    }
}
