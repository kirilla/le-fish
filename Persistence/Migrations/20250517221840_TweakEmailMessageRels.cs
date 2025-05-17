using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TweakEmailMessageRels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_EmailAccounts_EmailAccountId",
                table: "EmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Range",
                table: "IpRanges",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmailTargetId",
                table: "EmailMessages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmailAccountId",
                table: "EmailMessages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_EmailAccounts_EmailAccountId",
                table: "EmailMessages",
                column: "EmailAccountId",
                principalTable: "EmailAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_EmailAccounts_EmailAccountId",
                table: "EmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Range",
                table: "IpRanges",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<int>(
                name: "EmailTargetId",
                table: "EmailMessages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmailAccountId",
                table: "EmailMessages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_EmailAccounts_EmailAccountId",
                table: "EmailMessages",
                column: "EmailAccountId",
                principalTable: "EmailAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_EmailTargets_EmailTargetId",
                table: "EmailMessages",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhishingTokens_EmailTargets_EmailTargetId",
                table: "PhishingTokens",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
