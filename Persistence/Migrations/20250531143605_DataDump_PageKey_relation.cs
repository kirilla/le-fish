using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DataDump_PageKey_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataDumps_EmailTargets_EmailTargetId",
                table: "DataDumps");

            migrationBuilder.RenameColumn(
                name: "EmailTargetId",
                table: "DataDumps",
                newName: "PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_DataDumps_EmailTargetId",
                table: "DataDumps",
                newName: "IX_DataDumps_PageKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataDumps_PageKeys_PageKeyId",
                table: "DataDumps",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataDumps_PageKeys_PageKeyId",
                table: "DataDumps");

            migrationBuilder.RenameColumn(
                name: "PageKeyId",
                table: "DataDumps",
                newName: "EmailTargetId");

            migrationBuilder.RenameIndex(
                name: "IX_DataDumps_PageKeyId",
                table: "DataDumps",
                newName: "IX_DataDumps_EmailTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataDumps_EmailTargets_EmailTargetId",
                table: "DataDumps",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
