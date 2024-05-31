using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC.Migrations
{
    /// <inheritdoc />
    public partial class updateprofileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<int>>(
                name: "ScreenProfileFiltering_EntityIds",
                table: "ScreenProfiles",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScreenProfileFiltering_IsPickup",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScreenProfileFiltering_IsSale",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int[]>(
                name: "ScreenProfileFiltering_OrderStatusses",
                table: "ScreenProfiles",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScreenProfileFiltering_OrderTimeRange_EndDate",
                table: "ScreenProfiles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ScreenProfileFiltering_OrderTimeRange_StartDate",
                table: "ScreenProfiles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreenProfileFiltering_EntityIds",
                table: "ScreenProfiles");

            migrationBuilder.DropColumn(
                name: "ScreenProfileFiltering_IsPickup",
                table: "ScreenProfiles");

            migrationBuilder.DropColumn(
                name: "ScreenProfileFiltering_IsSale",
                table: "ScreenProfiles");

            migrationBuilder.DropColumn(
                name: "ScreenProfileFiltering_OrderStatusses",
                table: "ScreenProfiles");

            migrationBuilder.DropColumn(
                name: "ScreenProfileFiltering_OrderTimeRange_EndDate",
                table: "ScreenProfiles");

            migrationBuilder.DropColumn(
                name: "ScreenProfileFiltering_OrderTimeRange_StartDate",
                table: "ScreenProfiles");
        }
    }
}
