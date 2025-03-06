using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectBakary.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMig222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Menus_MenuId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_MenuId",
                table: "Offices");

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Menus",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MenuBreads",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    _availableBreadsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuBreads", x => new { x.MenuId, x._availableBreadsId });
                    table.ForeignKey(
                        name: "FK_MenuBreads_Breads__availableBreadsId",
                        column: x => x._availableBreadsId,
                        principalTable: "Breads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuBreads_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_OfficeId",
                table: "Menus",
                column: "OfficeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuBreads__availableBreadsId",
                table: "MenuBreads",
                column: "_availableBreadsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Offices_OfficeId",
                table: "Menus",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Offices_OfficeId",
                table: "Menus");

            migrationBuilder.DropTable(
                name: "MenuBreads");

            migrationBuilder.DropIndex(
                name: "IX_Menus_OfficeId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Menus");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_MenuId",
                table: "Offices",
                column: "MenuId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Menus_MenuId",
                table: "Offices",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
