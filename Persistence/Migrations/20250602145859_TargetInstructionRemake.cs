using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TargetInstructionRemake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetInstructions_Instructions_InstructionId",
                table: "TargetInstructions");

            migrationBuilder.DropIndex(
                name: "IX_TargetInstructions_InstructionId",
                table: "TargetInstructions");

            migrationBuilder.RenameColumn(
                name: "InstructionId",
                table: "TargetInstructions",
                newName: "Reference");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TargetInstructions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Script",
                table: "TargetInstructions",
                type: "nvarchar(max)",
                maxLength: 16000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TargetInstructions");

            migrationBuilder.DropColumn(
                name: "Script",
                table: "TargetInstructions");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "TargetInstructions",
                newName: "InstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetInstructions_InstructionId",
                table: "TargetInstructions",
                column: "InstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetInstructions_Instructions_InstructionId",
                table: "TargetInstructions",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
