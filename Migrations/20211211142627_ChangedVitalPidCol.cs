using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Migrations
{
    public partial class ChangedVitalPidCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vitals_Patients_PatientId",
                table: "Vitals");

            migrationBuilder.DropIndex(
                name: "IX_Vitals_PatientId",
                table: "Vitals");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Vitals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Vitals",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vitals_PatientId",
                table: "Vitals",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vitals_Patients_PatientId",
                table: "Vitals",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
