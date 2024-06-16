using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC.Migrations
{
    /// <inheritdoc />
    public partial class AddingHashToScreen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashToken",
                table: "Screens",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashToken",
                table: "Screens");
        }
    }
}
