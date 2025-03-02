using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkySearchWorker.Migrations
{
    /// <inheritdoc />
    public partial class AddTheArrivalAirportInFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_AirportId",
                table: "Flight");

            migrationBuilder.RenameColumn(
                name: "AirportId",
                table: "Flight",
                newName: "DepartureAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_AirportId",
                table: "Flight",
                newName: "IX_Flight_DepartureAirportId");

            migrationBuilder.AddColumn<int>(
                name: "ArrivalAirportId",
                table: "Flight",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Flight_ArrivalAirportId",
                table: "Flight",
                column: "ArrivalAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_ArrivalAirportId",
                table: "Flight",
                column: "ArrivalAirportId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_DepartureAirportId",
                table: "Flight",
                column: "DepartureAirportId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_ArrivalAirportId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_DepartureAirportId",
                table: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_Flight_ArrivalAirportId",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "ArrivalAirportId",
                table: "Flight");

            migrationBuilder.RenameColumn(
                name: "DepartureAirportId",
                table: "Flight",
                newName: "AirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_DepartureAirportId",
                table: "Flight",
                newName: "IX_Flight_AirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_AirportId",
                table: "Flight",
                column: "AirportId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
