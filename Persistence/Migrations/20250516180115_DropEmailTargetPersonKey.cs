using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DropEmailTargetPersonKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonKey",
                table: "EmailTargets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonKey",
                table: "EmailTargets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
