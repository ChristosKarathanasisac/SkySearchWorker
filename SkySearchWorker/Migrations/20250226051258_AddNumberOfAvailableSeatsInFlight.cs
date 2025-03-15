using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkySearchWorker.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOfAvailableSeatsInFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "currency",
                table: "FlightPrice",
                newName: "Currency");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAvailableSeats",
                table: "Flight",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfAvailableSeats",
                table: "Flight");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "FlightPrice",
                newName: "currency");
        }
    }
}
