using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ReservationsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassengersCount",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassengersCount",
                table: "Reservations");
        }
    }
}
