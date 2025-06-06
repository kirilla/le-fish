using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class WebPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_PayloadPages_PayloadPageId",
                table: "Attacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PayloadPages",
                table: "PayloadPages");

            migrationBuilder.RenameTable(
                name: "PayloadPages",
                newName: "WebPages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebPages",
                table: "WebPages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_WebPages_PayloadPageId",
                table: "Attacks",
                column: "PayloadPageId",
                principalTable: "WebPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_WebPages_PayloadPageId",
                table: "Attacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WebPages",
                table: "WebPages");

            migrationBuilder.RenameTable(
                name: "WebPages",
                newName: "PayloadPages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PayloadPages",
                table: "PayloadPages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_PayloadPages_PayloadPageId",
                table: "Attacks",
                column: "PayloadPageId",
                principalTable: "PayloadPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
