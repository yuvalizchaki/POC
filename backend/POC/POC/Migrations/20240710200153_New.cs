using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_To_Unit",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_TimeRanges_To_Unit");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_To_Mode",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_TimeRanges_To_Mode");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_To_Amount",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_TimeRanges_To_Amount");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_From_Unit",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_TimeRanges_From_Unit");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_From_Mode",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_TimeRanges_From_Mode");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_From_Amount",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_TimeRanges_From_Amount");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Screens",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<List<string>>(
                name: "ScreenProfileFiltering_InventorySorting",
                table: "ScreenProfiles",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_To_Unit",
                table: "ScreenProfiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_To_Mode",
                table: "ScreenProfiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_To_Amount",
                table: "ScreenProfiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_From_Unit",
                table: "ScreenProfiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_From_Mode",
                table: "ScreenProfiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_From_Amount",
                table: "ScreenProfiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_Include",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Screens");

            migrationBuilder.DropColumn(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_Include",
                table: "ScreenProfiles");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_To_Unit",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_To_Unit");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_To_Mode",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_To_Mode");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_To_Amount",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_To_Amount");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_From_Unit",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_From_Unit");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_From_Mode",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_From_Mode");

            migrationBuilder.RenameColumn(
                name: "ScreenProfileFiltering_OrderFiltering_TimeRanges_From_Amount",
                table: "ScreenProfiles",
                newName: "ScreenProfileFiltering_OrderFiltering_From_Amount");

            migrationBuilder.AlterColumn<List<string>>(
                name: "ScreenProfileFiltering_InventorySorting",
                table: "ScreenProfiles",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_To_Unit",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_To_Mode",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_To_Amount",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_From_Unit",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_From_Mode",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScreenProfileFiltering_OrderFiltering_From_Amount",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
