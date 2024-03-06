﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddMeTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class GuaranteedTourDatesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuaranteedTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TourEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxPeopleCount = table.Column<byte>(type: "tinyint", nullable: false),
                    ReservedPeopleCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuaranteedTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuaranteedTimes_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuaranteedTimes_TourId",
                table: "GuaranteedTimes",
                column: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuaranteedTimes");
        }
    }
}
