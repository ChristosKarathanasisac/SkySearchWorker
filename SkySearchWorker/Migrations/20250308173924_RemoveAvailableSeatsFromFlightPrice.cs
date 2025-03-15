using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkySearchWorker.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAvailableSeatsFromFlightPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableSeats",
                table: "FlightPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableSeats",
                table: "FlightPrice",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
