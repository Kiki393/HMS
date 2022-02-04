using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Migrations
{
    public partial class RemovedPaidFromTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Paid",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
