using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PhishingToken_EmailMessage_rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailMessageId",
                table: "PhishingTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhishingTokens_EmailMessageId",
                table: "PhishingTokens",
                column: "EmailMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhishingTokens_EmailMessages_EmailMessageId",
                table: "PhishingTokens",
                column: "EmailMessageId",
                principalTable: "EmailMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhishingTokens_EmailMessages_EmailMessageId",
                table: "PhishingTokens");

            migrationBuilder.DropIndex(
                name: "IX_PhishingTokens_EmailMessageId",
                table: "PhishingTokens");

            migrationBuilder.DropColumn(
                name: "EmailMessageId",
                table: "PhishingTokens");
        }
    }
}
