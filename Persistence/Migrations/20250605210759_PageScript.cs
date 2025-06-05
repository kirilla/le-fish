using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageScript : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_PayloadScripts_PayloadScriptId",
                table: "Attacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PayloadScripts",
                table: "PayloadScripts");

            migrationBuilder.RenameTable(
                name: "PayloadScripts",
                newName: "PageScripts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageScripts",
                table: "PageScripts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_PageScripts_PayloadScriptId",
                table: "Attacks",
                column: "PayloadScriptId",
                principalTable: "PageScripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_PageScripts_PayloadScriptId",
                table: "Attacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageScripts",
                table: "PageScripts");

            migrationBuilder.RenameTable(
                name: "PageScripts",
                newName: "PayloadScripts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PayloadScripts",
                table: "PayloadScripts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_PayloadScripts_PayloadScriptId",
                table: "Attacks",
                column: "PayloadScriptId",
                principalTable: "PayloadScripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
