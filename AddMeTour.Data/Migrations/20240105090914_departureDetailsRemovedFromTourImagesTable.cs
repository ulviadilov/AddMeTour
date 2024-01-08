using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddMeTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class departureDetailsRemovedFromTourImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureDetails",
                table: "TourImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartureDetails",
                table: "TourImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
