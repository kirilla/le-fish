using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TargetInstructions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueuedInstructions");

            migrationBuilder.CreateTable(
                name: "TargetInstructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InstructionId = table.Column<int>(type: "int", nullable: false),
                    PageKeyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TargetInstructions_Instructions_InstructionId",
                        column: x => x.InstructionId,
                        principalTable: "Instructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetInstructions_PageKeys_PageKeyId",
                        column: x => x.PageKeyId,
                        principalTable: "PageKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TargetInstructions_InstructionId",
                table: "TargetInstructions",
                column: "InstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetInstructions_PageKeyId",
                table: "TargetInstructions",
                column: "PageKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TargetInstructions");

            migrationBuilder.CreateTable(
                name: "QueuedInstructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructionId = table.Column<int>(type: "int", nullable: false),
                    PageKeyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuedInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueuedInstructions_Instructions_InstructionId",
                        column: x => x.InstructionId,
                        principalTable: "Instructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueuedInstructions_PageKeys_PageKeyId",
                        column: x => x.PageKeyId,
                        principalTable: "PageKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueuedInstructions_InstructionId",
                table: "QueuedInstructions",
                column: "InstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_QueuedInstructions_PageKeyId",
                table: "QueuedInstructions",
                column: "PageKeyId");
        }
    }
}
