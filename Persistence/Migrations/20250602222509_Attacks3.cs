using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Attacks3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataDumps_Attacks_PageKeyId",
                table: "DataDumps");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_Attacks_PageKeyId",
                table: "EmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_Attacks_PageKeyId",
                table: "PageVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_ScriptVisits_Attacks_PageKeyId",
                table: "ScriptVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetInstructions_Attacks_PageKeyId",
                table: "TargetInstructions");

            migrationBuilder.RenameColumn(
                name: "PageKeyId",
                table: "TargetInstructions",
                newName: "AttackId");

            migrationBuilder.RenameIndex(
                name: "IX_TargetInstructions_PageKeyId",
                table: "TargetInstructions",
                newName: "IX_TargetInstructions_AttackId");

            migrationBuilder.RenameColumn(
                name: "PageKeyId",
                table: "ScriptVisits",
                newName: "AttackId");

            migrationBuilder.RenameIndex(
                name: "IX_ScriptVisits_PageKeyId",
                table: "ScriptVisits",
                newName: "IX_ScriptVisits_AttackId");

            migrationBuilder.RenameColumn(
                name: "PageKeyId",
                table: "PageVisits",
                newName: "AttackId");

            migrationBuilder.RenameIndex(
                name: "IX_PageVisits_PageKeyId",
                table: "PageVisits",
                newName: "IX_PageVisits_AttackId");

            migrationBuilder.RenameColumn(
                name: "PageKeyId",
                table: "EmailMessages",
                newName: "AttackId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailMessages_PageKeyId",
                table: "EmailMessages",
                newName: "IX_EmailMessages_AttackId");

            migrationBuilder.RenameColumn(
                name: "PageKeyId",
                table: "DataDumps",
                newName: "AttackId");

            migrationBuilder.RenameIndex(
                name: "IX_DataDumps_PageKeyId",
                table: "DataDumps",
                newName: "IX_DataDumps_AttackId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataDumps_Attacks_AttackId",
                table: "DataDumps",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_Attacks_AttackId",
                table: "EmailMessages",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_Attacks_AttackId",
                table: "PageVisits",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptVisits_Attacks_AttackId",
                table: "ScriptVisits",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetInstructions_Attacks_AttackId",
                table: "TargetInstructions",
                column: "AttackId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataDumps_Attacks_AttackId",
                table: "DataDumps");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_Attacks_AttackId",
                table: "EmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_Attacks_AttackId",
                table: "PageVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_ScriptVisits_Attacks_AttackId",
                table: "ScriptVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetInstructions_Attacks_AttackId",
                table: "TargetInstructions");

            migrationBuilder.RenameColumn(
                name: "AttackId",
                table: "TargetInstructions",
                newName: "PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_TargetInstructions_AttackId",
                table: "TargetInstructions",
                newName: "IX_TargetInstructions_PageKeyId");

            migrationBuilder.RenameColumn(
                name: "AttackId",
                table: "ScriptVisits",
                newName: "PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_ScriptVisits_AttackId",
                table: "ScriptVisits",
                newName: "IX_ScriptVisits_PageKeyId");

            migrationBuilder.RenameColumn(
                name: "AttackId",
                table: "PageVisits",
                newName: "PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_PageVisits_AttackId",
                table: "PageVisits",
                newName: "IX_PageVisits_PageKeyId");

            migrationBuilder.RenameColumn(
                name: "AttackId",
                table: "EmailMessages",
                newName: "PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailMessages_AttackId",
                table: "EmailMessages",
                newName: "IX_EmailMessages_PageKeyId");

            migrationBuilder.RenameColumn(
                name: "AttackId",
                table: "DataDumps",
                newName: "PageKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_DataDumps_AttackId",
                table: "DataDumps",
                newName: "IX_DataDumps_PageKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataDumps_Attacks_PageKeyId",
                table: "DataDumps",
                column: "PageKeyId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_Attacks_PageKeyId",
                table: "EmailMessages",
                column: "PageKeyId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_Attacks_PageKeyId",
                table: "PageVisits",
                column: "PageKeyId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptVisits_Attacks_PageKeyId",
                table: "ScriptVisits",
                column: "PageKeyId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetInstructions_Attacks_PageKeyId",
                table: "TargetInstructions",
                column: "PageKeyId",
                principalTable: "Attacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
