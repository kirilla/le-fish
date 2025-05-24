using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageKeyValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "PageKeys",
                newName: "Value");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_Token",
                table: "PageKeys",
                newName: "IX_PageKeys_Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "PageKeys",
                newName: "Token");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_Value",
                table: "PageKeys",
                newName: "IX_PageKeys_Token");
        }
    }
}
