using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Migrations
{
    public partial class RemovedForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    Bp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false)
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
    }
}
