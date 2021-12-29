using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Migrations
{
    public partial class AddNHISToPatients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NHIS",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NHIS",
                table: "Patients");
        }
    }
}
