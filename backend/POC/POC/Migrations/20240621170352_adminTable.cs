using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace POC.Migrations
{
    /// <inheritdoc />
    public partial class adminTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Username = table.Column<string>(type: "text", nullable: false),
                    HashedPassword = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "ScreenProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    ScreenProfileFiltering_OrderFiltering_From_Unit = table.Column<int>(type: "integer", nullable: false),
                    ScreenProfileFiltering_OrderFiltering_From_Mode = table.Column<int>(type: "integer", nullable: false),
                    ScreenProfileFiltering_OrderFiltering_From_Amount = table.Column<int>(type: "integer", nullable: false),
                    ScreenProfileFiltering_OrderFiltering_To_Unit = table.Column<int>(type: "integer", nullable: false),
                    ScreenProfileFiltering_OrderFiltering_To_Mode = table.Column<int>(type: "integer", nullable: false),
                    ScreenProfileFiltering_OrderFiltering_To_Amount = table.Column<int>(type: "integer", nullable: false),
                    ScreenProfileFiltering_OrderFiltering_OrderStatuses = table.Column<int[]>(type: "integer[]", nullable: true),
                    ScreenProfileFiltering_OrderFiltering_IsSale = table.Column<bool>(type: "boolean", nullable: true),
                    ScreenProfileFiltering_OrderFiltering_IsPickup = table.Column<bool>(type: "boolean", nullable: true),
                    ScreenProfileFiltering_OrderFiltering_EntityIds = table.Column<List<int>>(type: "integer[]", nullable: true),
                    ScreenProfileFiltering_OrderFiltering_Tags = table.Column<int[]>(type: "integer[]", nullable: true),
                    ScreenProfileFiltering_InventoryFiltering_EntityIds = table.Column<List<int>>(type: "integer[]", nullable: true),
                    ScreenProfileFiltering_InventorySorting = table.Column<List<string>>(type: "text[]", nullable: false),
                    ScreenProfileFiltering_DisplayConfig_IsPaging = table.Column<bool>(type: "boolean", nullable: false),
                    ScreenProfileFiltering_DisplayConfig_DisplayTemplate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScreenProfileId = table.Column<int>(type: "integer", nullable: false),
                    HashToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screens_ScreenProfiles_ScreenProfileId",
                        column: x => x.ScreenProfileId,
                        principalTable: "ScreenProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Screens_ScreenProfileId",
                table: "Screens",
                column: "ScreenProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropTable(
                name: "ScreenProfiles");
        }
    }
}
