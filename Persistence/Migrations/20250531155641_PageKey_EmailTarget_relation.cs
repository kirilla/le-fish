using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageKey_EmailTarget_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages");

            migrationBuilder.AddColumn<int>(
                name: "EmailTargetId",
                table: "PageKeys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PageKeys_EmailTargetId",
                table: "PageKeys",
                column: "EmailTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageKeys_EmailTargets_EmailTargetId",
                table: "PageKeys",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_PageKeys_EmailTargets_EmailTargetId",
                table: "PageKeys");

            migrationBuilder.DropIndex(
                name: "IX_PageKeys_EmailTargetId",
                table: "PageKeys");

            migrationBuilder.DropColumn(
                name: "EmailTargetId",
                table: "PageKeys");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
