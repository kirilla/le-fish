using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PhishinKeyRemake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlRegex",
                table: "PayloadPages");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "EmailTargets");

            migrationBuilder.AddColumn<int>(
                name: "PageKey",
                table: "PayloadPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonKey",
                table: "EmailTargets",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageKey",
                table: "PayloadPages");

            migrationBuilder.DropColumn(
                name: "PersonKey",
                table: "EmailTargets");

            migrationBuilder.AddColumn<string>(
                name: "UrlRegex",
                table: "PayloadPages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "EmailTargets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
