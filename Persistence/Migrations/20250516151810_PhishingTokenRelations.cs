using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PhishingTokenRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens");

            migrationBuilder.AlterColumn<int>(
                name: "Token",
                table: "PhishingTokens",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "EmailTargetId",
                table: "PhishingTokens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayloadPageId",
                table: "PhishingTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PayloadPages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhishingTokens_PayloadPageId",
                table: "PhishingTokens",
                column: "PayloadPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhishingTokens_PayloadPages_PayloadPageId",
                table: "PhishingTokens",
                column: "PayloadPageId",
                principalTable: "PayloadPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_PhishingTokens_PayloadPages_PayloadPageId",
                table: "PhishingTokens");

            migrationBuilder.DropIndex(
                name: "IX_PhishingTokens_PayloadPageId",
                table: "PhishingTokens");

            migrationBuilder.DropColumn(
                name: "PayloadPageId",
                table: "PhishingTokens");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "PayloadPages");

            migrationBuilder.AlterColumn<long>(
                name: "Token",
                table: "PhishingTokens",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmailTargetId",
                table: "PhishingTokens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id");
        }
    }
}
