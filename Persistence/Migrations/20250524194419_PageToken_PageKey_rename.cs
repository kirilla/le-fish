using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageToken_PageKey_rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageTokens_EmailMessages_EmailMessageId",
                table: "PageTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_PageTokens_PayloadPages_PayloadPageId",
                table: "PageTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_PageTokens_PayloadScripts_PayloadScriptId",
                table: "PageTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_PageTokens_PageTokenId",
                table: "PageVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_ScriptVisits_PageTokens_PageTokenId",
                table: "ScriptVisits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageTokens",
                table: "PageTokens");

            migrationBuilder.RenameTable(
                name: "PageTokens",
                newName: "PageKeys");

            migrationBuilder.RenameIndex(
                name: "IX_PageTokens_Token",
                table: "PageKeys",
                newName: "IX_PageKeys_Token");

            migrationBuilder.RenameIndex(
                name: "IX_PageTokens_PayloadScriptId",
                table: "PageKeys",
                newName: "IX_PageKeys_PayloadScriptId");

            migrationBuilder.RenameIndex(
                name: "IX_PageTokens_PayloadPageId",
                table: "PageKeys",
                newName: "IX_PageKeys_PayloadPageId");

            migrationBuilder.RenameIndex(
                name: "IX_PageTokens_EmailMessageId",
                table: "PageKeys",
                newName: "IX_PageKeys_EmailMessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageKeys",
                table: "PageKeys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageKeys_EmailMessages_EmailMessageId",
                table: "PageKeys",
                column: "EmailMessageId",
                principalTable: "EmailMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageKeys_PayloadPages_PayloadPageId",
                table: "PageKeys",
                column: "PayloadPageId",
                principalTable: "PayloadPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageKeys_PayloadScripts_PayloadScriptId",
                table: "PageKeys",
                column: "PayloadScriptId",
                principalTable: "PayloadScripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_PageKeys_PageTokenId",
                table: "PageVisits",
                column: "PageTokenId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptVisits_PageKeys_PageTokenId",
                table: "ScriptVisits",
                column: "PageTokenId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageKeys_EmailMessages_EmailMessageId",
                table: "PageKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_PageKeys_PayloadPages_PayloadPageId",
                table: "PageKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_PageKeys_PayloadScripts_PayloadScriptId",
                table: "PageKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_PageKeys_PageTokenId",
                table: "PageVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_ScriptVisits_PageKeys_PageTokenId",
                table: "ScriptVisits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageKeys",
                table: "PageKeys");

            migrationBuilder.RenameTable(
                name: "PageKeys",
                newName: "PageTokens");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_Token",
                table: "PageTokens",
                newName: "IX_PageTokens_Token");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_PayloadScriptId",
                table: "PageTokens",
                newName: "IX_PageTokens_PayloadScriptId");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_PayloadPageId",
                table: "PageTokens",
                newName: "IX_PageTokens_PayloadPageId");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_EmailMessageId",
                table: "PageTokens",
                newName: "IX_PageTokens_EmailMessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageTokens",
                table: "PageTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageTokens_EmailMessages_EmailMessageId",
                table: "PageTokens",
                column: "EmailMessageId",
                principalTable: "EmailMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageTokens_PayloadPages_PayloadPageId",
                table: "PageTokens",
                column: "PayloadPageId",
                principalTable: "PayloadPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageTokens_PayloadScripts_PayloadScriptId",
                table: "PageTokens",
                column: "PayloadScriptId",
                principalTable: "PayloadScripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_PageTokens_PageTokenId",
                table: "PageVisits",
                column: "PageTokenId",
                principalTable: "PageTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptVisits_PageTokens_PageTokenId",
                table: "ScriptVisits",
                column: "PageTokenId",
                principalTable: "PageTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
