using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddMeTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsBestAddedToToursTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBest",
                table: "Tours",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBest",
                table: "Tours");
        }
    }
}
