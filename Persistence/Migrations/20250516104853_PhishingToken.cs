using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PhishingToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhishingTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailTargetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhishingTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                        column: x => x.EmailTargetId,
                        principalTable: "EmailTargets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhishingTokens_EmailTargetId",
                table: "PhishingTokens",
                column: "EmailTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_PhishingTokens_Token",
                table: "PhishingTokens",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhishingTokens");
        }
    }
}
