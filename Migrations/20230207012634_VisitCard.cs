using Microsoft.EntityFrameworkCore.Migrations;

namespace VisitCardEfCore.Migrations
{
    public partial class VisitCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "VisitCards",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "VisitCards");
        }
    }
}
