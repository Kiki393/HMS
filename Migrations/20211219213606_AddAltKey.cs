using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Migrations
{
    public partial class AddAltKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Patients_PatientId",
                table: "Patients",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Patients_PatientId",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
