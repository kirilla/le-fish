using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageScriptId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_PageScripts_PayloadScriptId",
                table: "Attacks");

            migrationBuilder.RenameColumn(
                name: "PayloadScriptId",
                table: "Attacks",
                newName: "PageScriptId");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_PayloadScriptId",
                table: "Attacks",
                newName: "IX_Attacks_PageScriptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_PageScripts_PageScriptId",
                table: "Attacks",
                column: "PageScriptId",
                principalTable: "PageScripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_PageScripts_PageScriptId",
                table: "Attacks");

            migrationBuilder.RenameColumn(
                name: "PageScriptId",
                table: "Attacks",
                newName: "PayloadScriptId");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_PageScriptId",
                table: "Attacks",
                newName: "IX_Attacks_PayloadScriptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_PageScripts_PayloadScriptId",
                table: "Attacks",
                column: "PayloadScriptId",
                principalTable: "PageScripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
