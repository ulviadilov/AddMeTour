using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddMeTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class TourUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourCategories_Categories_CategoryId",
                table: "TourCategories");

            migrationBuilder.AddColumn<string>(
                name: "PosterImageUrl",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TourLanguages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TourInclusions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DepartureDetails",
                table: "TourImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TourExclusions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TourCountries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "TourCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TourCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TourCategories_Categories_CategoryId",
                table: "TourCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourCategories_Categories_CategoryId",
                table: "TourCategories");

            migrationBuilder.DropColumn(
                name: "PosterImageUrl",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TourLanguages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TourInclusions");

            migrationBuilder.DropColumn(
                name: "DepartureDetails",
                table: "TourImages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TourExclusions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TourCountries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TourCategories");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "TourCategories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TourCategories_Categories_CategoryId",
                table: "TourCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
