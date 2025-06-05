using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TargetId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_Targets_EmailTargetId",
                table: "Attacks");

            migrationBuilder.RenameColumn(
                name: "EmailTargetId",
                table: "Attacks",
                newName: "TargetId");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_EmailTargetId",
                table: "Attacks",
                newName: "IX_Attacks_TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_Targets_TargetId",
                table: "Attacks",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_Targets_TargetId",
                table: "Attacks");

            migrationBuilder.RenameColumn(
                name: "TargetId",
                table: "Attacks",
                newName: "EmailTargetId");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_TargetId",
                table: "Attacks",
                newName: "IX_Attacks_EmailTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_Targets_EmailTargetId",
                table: "Attacks",
                column: "EmailTargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
