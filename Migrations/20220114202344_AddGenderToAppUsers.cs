using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Migrations
{
    public partial class AddGenderToAppUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Patients",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "NHIS",
                table: "Patients",
                newName: "Nhis");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Nhis",
                table: "Patients",
                newName: "NHIS");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Patients",
                newName: "gender");
        }
    }
}
