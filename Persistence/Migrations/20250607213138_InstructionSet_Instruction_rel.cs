using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InstructionSet_Instruction_rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructionSetId",
                table: "Instructions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_InstructionSetId",
                table: "Instructions",
                column: "InstructionSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_InstructionSets_InstructionSetId",
                table: "Instructions",
                column: "InstructionSetId",
                principalTable: "InstructionSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_InstructionSets_InstructionSetId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_InstructionSetId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "InstructionSetId",
                table: "Instructions");
        }
    }
}
