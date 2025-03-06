using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectBakary.Migrations
{
    /// <inheritdoc />
    public partial class SecondMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Menus_MenuId",
                table: "Offices");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Offices_OfficeId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Offices_OfficeId1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Offices_MenuId",
                table: "Offices");

            migrationBuilder.RenameColumn(
                name: "OfficeId1",
                table: "Order",
                newName: "PendingOfficeId");

            migrationBuilder.RenameColumn(
                name: "OfficeId",
                table: "Order",
                newName: "FinishedOfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OfficeId1",
                table: "Order",
                newName: "IX_Order_PendingOfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OfficeId",
                table: "Order",
                newName: "IX_Order_FinishedOfficeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Offices_FinishedOfficeId",
                table: "Order",
                column: "FinishedOfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Offices_PendingOfficeId",
                table: "Order",
                column: "PendingOfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Menus_MenuId",
                table: "Offices");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Offices_FinishedOfficeId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Offices_PendingOfficeId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Offices_MenuId",
                table: "Offices");

            migrationBuilder.RenameColumn(
                name: "PendingOfficeId",
                table: "Order",
                newName: "OfficeId1");

            migrationBuilder.RenameColumn(
                name: "FinishedOfficeId",
                table: "Order",
                newName: "OfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PendingOfficeId",
                table: "Order",
                newName: "IX_Order_OfficeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Order_FinishedOfficeId",
                table: "Order",
                newName: "IX_Order_OfficeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Offices_OfficeId",
                table: "Order",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Offices_OfficeId1",
                table: "Order",
                column: "OfficeId1",
                principalTable: "Offices",
                principalColumn: "Id");
        }
    }
}
