using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Migrations
{
    public partial class AddVitals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bp",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "PatientVitals",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PatientVitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Bp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVitals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientVitals",
                table: "Patients",
                column: "PatientVitals");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_PatientVitals_PatientVitals",
                table: "Patients",
                column: "PatientVitals",
                principalTable: "PatientVitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_PatientVitals_PatientVitals",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "PatientVitals");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientVitals",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientVitals",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "Bp",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Patients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "weight",
                table: "Patients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
