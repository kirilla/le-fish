using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class WebPageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_WebPages_PayloadPageId",
                table: "Attacks");

            migrationBuilder.RenameColumn(
                name: "PayloadPageId",
                table: "Attacks",
                newName: "WebPageId");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_PayloadPageId",
                table: "Attacks",
                newName: "IX_Attacks_WebPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_WebPages_WebPageId",
                table: "Attacks",
                column: "WebPageId",
                principalTable: "WebPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_WebPages_WebPageId",
                table: "Attacks");

            migrationBuilder.RenameColumn(
                name: "WebPageId",
                table: "Attacks",
                newName: "PayloadPageId");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_WebPageId",
                table: "Attacks",
                newName: "IX_Attacks_PayloadPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_WebPages_PayloadPageId",
                table: "Attacks",
                column: "PayloadPageId",
                principalTable: "WebPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
