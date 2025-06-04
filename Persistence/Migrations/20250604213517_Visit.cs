using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Visit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_Attacks_AttackId",
                table: "PageVisits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageVisits",
                table: "PageVisits");

            migrationBuilder.RenameTable(
                name: "PageVisits",
                newName: "Visits");

            migrationBuilder.RenameIndex(
                name: "IX_PageVisits_AttackId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Attacks_AttackId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visits",
                table: "Visits");

            migrationBuilder.RenameTable(
                name: "Visits",
                newName: "PageVisits");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_AttackId",
                table: "PageVisits",
                newName: "IX_PageVisits_AttackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageVisits",
                table: "PageVisits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_Attacks_AttackId",
                table: "PageVisits",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
