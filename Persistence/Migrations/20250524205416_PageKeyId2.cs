using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageKeyId2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScriptVisits_PageKeys_PageTokenId",
                table: "ScriptVisits");

            migrationBuilder.RenameColumn(
                name: "PageTokenId",
                table: "ScriptVisits",
                newName: "PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_ScriptVisits_PageTokenId",
                table: "ScriptVisits",
                newName: "IX_ScriptVisits_PageKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptVisits_PageKeys_PageKeyId",
                table: "ScriptVisits",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScriptVisits_PageKeys_PageKeyId",
                table: "ScriptVisits");

            migrationBuilder.RenameColumn(
                name: "PageKeyId",
                table: "ScriptVisits",
                newName: "PageTokenId");

            migrationBuilder.RenameIndex(
                name: "IX_ScriptVisits_PageKeyId",
                table: "ScriptVisits",
                newName: "IX_ScriptVisits_PageTokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptVisits_PageKeys_PageTokenId",
                table: "ScriptVisits",
                column: "PageTokenId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
