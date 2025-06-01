using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DataDumpJsonData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentLength",
                table: "DataDumps");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "DataDumps");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "DataDumps");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DataDumps");

            migrationBuilder.AddColumn<string>(
                name: "JsonData",
                table: "DataDumps",
                type: "nvarchar(max)",
                maxLength: 16000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonData",
                table: "DataDumps");

            migrationBuilder.AddColumn<int>(
                name: "ContentLength",
                table: "DataDumps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "DataDumps",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "DataDumps",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DataDumps",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
