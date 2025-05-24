using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageKeyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_PageKeys_PageTokenId",
                table: "PageVisits");

            migrationBuilder.RenameColumn(
                name: "PageTokenId",
                table: "PageVisits",
                newName: "PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_PageVisits_PageTokenId",
                table: "PageVisits",
                newName: "IX_PageVisits_PageKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_PageKeys_PageKeyId",
                table: "PageVisits",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_PageKeys_PageKeyId",
                table: "PageVisits");

            migrationBuilder.RenameColumn(
                name: "PageKeyId",
                table: "PageVisits",
                newName: "PageTokenId");

            migrationBuilder.RenameIndex(
                name: "IX_PageVisits_PageKeyId",
                table: "PageVisits",
                newName: "IX_PageVisits_PageTokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_PageKeys_PageTokenId",
                table: "PageVisits",
                column: "PageTokenId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
