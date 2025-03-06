using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectBakary.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMig2222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Offices_OfficeId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_OfficeId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Menus");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_MenuId",
                table: "Offices",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Menus_MenuId",
                table: "Offices",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Menus_OfficeId",
                table: "Menus",
                column: "OfficeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Offices_OfficeId",
                table: "Menus",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
