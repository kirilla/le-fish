using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AttackEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Attacks_AttackId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visits",
                table: "Visits");

            migrationBuilder.RenameTable(
                name: "Visits",
                newName: "AttackEvents");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_AttackId",
                table: "AttackEvents",
                newName: "IX_AttackEvents_AttackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttackEvents",
                table: "AttackEvents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttackEvents_Attacks_AttackId",
                table: "AttackEvents",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttackEvents_Attacks_AttackId",
                table: "AttackEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttackEvents",
                table: "AttackEvents");

            migrationBuilder.RenameTable(
                name: "AttackEvents",
                newName: "Visits");

            migrationBuilder.RenameIndex(
                name: "IX_AttackEvents_AttackId",
                table: "Visits",
                newName: "IX_Visits_AttackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visits",
                table: "Visits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Attacks_AttackId",
                table: "Visits",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
