using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PageToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhishingTokens");

            migrationBuilder.CreateTable(
                name: "PageTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailMessageId = table.Column<int>(type: "int", nullable: false),
                    PayloadPageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageTokens_EmailMessages_EmailMessageId",
                        column: x => x.EmailMessageId,
                        principalTable: "EmailMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageTokens_PayloadPages_PayloadPageId",
                        column: x => x.PayloadPageId,
                        principalTable: "PayloadPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageTokens_EmailMessageId",
                table: "PageTokens",
                column: "EmailMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageTokens_PayloadPageId",
                table: "PageTokens",
                column: "PayloadPageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageTokens_Token",
                table: "PageTokens",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageTokens");

            migrationBuilder.CreateTable(
                name: "PhishingTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailMessageId = table.Column<int>(type: "int", nullable: false),
                    PayloadPageId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Token = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhishingTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhishingTokens_EmailMessages_EmailMessageId",
                        column: x => x.EmailMessageId,
                        principalTable: "EmailMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhishingTokens_PayloadPages_PayloadPageId",
                        column: x => x.PayloadPageId,
                        principalTable: "PayloadPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhishingTokens_EmailMessageId",
                table: "PhishingTokens",
                column: "EmailMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_PhishingTokens_PayloadPageId",
                table: "PhishingTokens",
                column: "PayloadPageId");

            migrationBuilder.CreateIndex(
                name: "IX_PhishingTokens_Token",
                table: "PhishingTokens",
                column: "Token",
                unique: true);
        }
    }
}
