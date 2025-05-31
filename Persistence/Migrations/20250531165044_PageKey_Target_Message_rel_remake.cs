using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageKey_Target_Message_rel_remake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_PageKeys_EmailMessages_EmailMessageId",
                table: "PageKeys");

            migrationBuilder.DropIndex(
                name: "IX_PageKeys_EmailMessageId",
                table: "PageKeys");

            migrationBuilder.DropColumn(
                name: "EmailMessageId",
                table: "PageKeys");

            migrationBuilder.RenameColumn(
                name: "EmailTargetId",
                table: "EmailMessages",
                newName: "PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailMessages_EmailTargetId",
                table: "EmailMessages",
                newName: "IX_EmailMessages_PageKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_PageKeys_PageKeyId",
                table: "EmailMessages",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_PageKeys_PageKeyId",
                table: "EmailMessages");

            migrationBuilder.RenameColumn(
                name: "PageKeyId",
                table: "EmailMessages",
                newName: "EmailTargetId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailMessages_PageKeyId",
                table: "EmailMessages",
                newName: "IX_EmailMessages_EmailTargetId");

            migrationBuilder.AddColumn<int>(
                name: "EmailMessageId",
                table: "PageKeys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PageKeys_EmailMessageId",
                table: "PageKeys",
                column: "EmailMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageKeys_EmailMessages_EmailMessageId",
                table: "PageKeys",
                column: "EmailMessageId",
                principalTable: "EmailMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
