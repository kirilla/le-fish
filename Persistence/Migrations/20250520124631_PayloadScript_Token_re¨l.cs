using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PayloadScript_Token_rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayloadScriptId",
                table: "PageTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PageTokens_PayloadScriptId",
                table: "PageTokens",
                column: "PayloadScriptId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageTokens_PayloadScripts_PayloadScriptId",
                table: "PageTokens",
                column: "PayloadScriptId",
                principalTable: "PayloadScripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageTokens_PayloadScripts_PayloadScriptId",
                table: "PageTokens");

            migrationBuilder.DropIndex(
                name: "IX_PageTokens_PayloadScriptId",
                table: "PageTokens");

            migrationBuilder.DropColumn(
                name: "PayloadScriptId",
                table: "PageTokens");
        }
    }
}
