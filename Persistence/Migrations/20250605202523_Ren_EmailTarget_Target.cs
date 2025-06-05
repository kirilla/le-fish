using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Ren_EmailTarget_Target : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_EmailTargets_EmailTargetId",
                table: "Attacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTargets",
                table: "EmailTargets");

            migrationBuilder.RenameTable(
                name: "EmailTargets",
                newName: "Targets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Targets",
                table: "Targets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_Targets_EmailTargetId",
                table: "Attacks",
                column: "EmailTargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_Targets_EmailTargetId",
                table: "Attacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Targets",
                table: "Targets");

            migrationBuilder.RenameTable(
                name: "Targets",
                newName: "EmailTargets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTargets",
                table: "EmailTargets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_EmailTargets_EmailTargetId",
                table: "Attacks",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
