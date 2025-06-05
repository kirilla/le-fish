using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DataResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataDumps_Attacks_AttackId",
                table: "DataDumps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataDumps",
                table: "DataDumps");

            migrationBuilder.RenameTable(
                name: "DataDumps",
                newName: "DataResults");

            migrationBuilder.RenameIndex(
                name: "IX_DataDumps_AttackId",
                table: "DataResults",
                newName: "IX_DataResults_AttackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataResults",
                table: "DataResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataResults_Attacks_AttackId",
                table: "DataResults",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataResults_Attacks_AttackId",
                table: "DataResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataResults",
                table: "DataResults");

            migrationBuilder.RenameTable(
                name: "DataResults",
                newName: "DataDumps");

            migrationBuilder.RenameIndex(
                name: "IX_DataResults_AttackId",
                table: "DataDumps",
                newName: "IX_DataDumps_AttackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataDumps",
                table: "DataDumps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataDumps_Attacks_AttackId",
                table: "DataDumps",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
