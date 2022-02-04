using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Migrations
{
    public partial class RemovedNameFromTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
