using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC.Migrations
{
    /// <inheritdoc />
    public partial class New2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScreenProfiles_ScreenProfiles_TimeEncapsulatedOrderFilterin~",
                table: "ScreenProfiles");

            migrationBuilder.DropColumn(
                name: "TimeEncapsulatedOrderFilteringScreenProfileFilteringScreenProf~",
                table: "ScreenProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeEncapsulatedOrderFilteringScreenProfileFilteringScreenProf~",
                table: "ScreenProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenProfiles_ScreenProfiles_TimeEncapsulatedOrderFilterin~",
                table: "ScreenProfiles",
                column: "TimeEncapsulatedOrderFilteringScreenProfileFilteringScreenProf~",
                principalTable: "ScreenProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
