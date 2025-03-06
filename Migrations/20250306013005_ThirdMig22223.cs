using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectBakary.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMig22223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuBreads_Breads__availableBreadsId",
                table: "MenuBreads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuBreads",
                table: "MenuBreads");

            migrationBuilder.DropIndex(
                name: "IX_MenuBreads__availableBreadsId",
                table: "MenuBreads");

            migrationBuilder.RenameColumn(
                name: "_availableBreadsId",
                table: "MenuBreads",
                newName: "AvailableBreadsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuBreads",
                table: "MenuBreads",
                columns: new[] { "AvailableBreadsId", "MenuId" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuBreads_MenuId",
                table: "MenuBreads",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuBreads_Breads_AvailableBreadsId",
                table: "MenuBreads",
                column: "AvailableBreadsId",
                principalTable: "Breads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuBreads_Breads_AvailableBreadsId",
                table: "MenuBreads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuBreads",
                table: "MenuBreads");

            migrationBuilder.DropIndex(
                name: "IX_MenuBreads_MenuId",
                table: "MenuBreads");

            migrationBuilder.RenameColumn(
                name: "AvailableBreadsId",
                table: "MenuBreads",
                newName: "_availableBreadsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuBreads",
                table: "MenuBreads",
                columns: new[] { "MenuId", "_availableBreadsId" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuBreads__availableBreadsId",
                table: "MenuBreads",
                column: "_availableBreadsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuBreads_Breads__availableBreadsId",
                table: "MenuBreads",
                column: "_availableBreadsId",
                principalTable: "Breads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
