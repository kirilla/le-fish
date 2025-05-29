using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class QueuedInstructions_rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueuedInstruction_Instructions_InstructionId",
                table: "QueuedInstruction");

            migrationBuilder.DropForeignKey(
                name: "FK_QueuedInstruction_PageKeys_PageKeyId",
                table: "QueuedInstruction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QueuedInstruction",
                table: "QueuedInstruction");

            migrationBuilder.RenameTable(
                name: "QueuedInstruction",
                newName: "QueuedInstructions");

            migrationBuilder.RenameIndex(
                name: "IX_QueuedInstruction_PageKeyId",
                table: "QueuedInstructions",
                newName: "IX_QueuedInstructions_PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_QueuedInstruction_InstructionId",
                table: "QueuedInstructions",
                newName: "IX_QueuedInstructions_InstructionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueuedInstructions",
                table: "QueuedInstructions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QueuedInstructions_Instructions_InstructionId",
                table: "QueuedInstructions",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueuedInstructions_PageKeys_PageKeyId",
                table: "QueuedInstructions",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueuedInstructions_Instructions_InstructionId",
                table: "QueuedInstructions");

            migrationBuilder.DropForeignKey(
                name: "FK_QueuedInstructions_PageKeys_PageKeyId",
                table: "QueuedInstructions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QueuedInstructions",
                table: "QueuedInstructions");

            migrationBuilder.RenameTable(
                name: "QueuedInstructions",
                newName: "QueuedInstruction");

            migrationBuilder.RenameIndex(
                name: "IX_QueuedInstructions_PageKeyId",
                table: "QueuedInstruction",
                newName: "IX_QueuedInstruction_PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_QueuedInstructions_InstructionId",
                table: "QueuedInstruction",
                newName: "IX_QueuedInstruction_InstructionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueuedInstruction",
                table: "QueuedInstruction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QueuedInstruction_Instructions_InstructionId",
                table: "QueuedInstruction",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueuedInstruction_PageKeys_PageKeyId",
                table: "QueuedInstruction",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
