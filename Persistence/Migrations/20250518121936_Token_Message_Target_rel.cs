using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Token_Message_Target_rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens");

            migrationBuilder.DropIndex(
                name: "IX_PhishingTokens_EmailTargetId",
                table: "PhishingTokens");

            migrationBuilder.DropColumn(
                name: "EmailTargetId",
                table: "PhishingTokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailTargetId",
                table: "PhishingTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhishingTokens_EmailTargetId",
                table: "PhishingTokens",
                column: "EmailTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id");
        }
    }
}
