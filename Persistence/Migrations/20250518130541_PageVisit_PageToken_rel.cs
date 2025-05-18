using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageVisit_PageToken_rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_EmailTargets_EmailTargetId",
                table: "PageVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_PayloadPages_PayloadPageId",
                table: "PageVisits");

            migrationBuilder.DropIndex(
                name: "IX_PageVisits_EmailTargetId",
                table: "PageVisits");

            migrationBuilder.DropColumn(
                name: "EmailTargetId",
                table: "PageVisits");

            migrationBuilder.RenameColumn(
                name: "PayloadPageId",
                table: "PageVisits",
                newName: "PageTokenId");

            migrationBuilder.RenameIndex(
                name: "IX_PageVisits_PayloadPageId",
                table: "PageVisits",
                newName: "IX_PageVisits_PageTokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_PageTokens_PageTokenId",
                table: "PageVisits",
                column: "PageTokenId",
                principalTable: "PageTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_PageTokens_PageTokenId",
                table: "PageVisits");

            migrationBuilder.RenameColumn(
                name: "PageTokenId",
                table: "PageVisits",
                newName: "PayloadPageId");

            migrationBuilder.RenameIndex(
                name: "IX_PageVisits_PageTokenId",
                table: "PageVisits",
                newName: "IX_PageVisits_PayloadPageId");

            migrationBuilder.AddColumn<int>(
                name: "EmailTargetId",
                table: "PageVisits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PageVisits_EmailTargetId",
                table: "PageVisits",
                column: "EmailTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_EmailTargets_EmailTargetId",
                table: "PageVisits",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_PayloadPages_PayloadPageId",
                table: "PageVisits",
                column: "PayloadPageId",
                principalTable: "PayloadPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
