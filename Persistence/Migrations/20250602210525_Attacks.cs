using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lefish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Attacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataDumps_PageKeys_PageKeyId",
                table: "DataDumps");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_PageKeys_PageKeyId",
                table: "EmailMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_PageKeys_EmailTargets_EmailTargetId",
                table: "PageKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_PageKeys_PayloadPages_PayloadPageId",
                table: "PageKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_PageKeys_PayloadScripts_PayloadScriptId",
                table: "PageKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_PageVisits_PageKeys_PageKeyId",
                table: "PageVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_ScriptVisits_PageKeys_PageKeyId",
                table: "ScriptVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetInstructions_PageKeys_PageKeyId",
                table: "TargetInstructions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageKeys",
                table: "PageKeys");

            migrationBuilder.RenameTable(
                name: "PageKeys",
                newName: "Attacks");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_Value",
                table: "Attacks",
                newName: "IX_Attacks_Value");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_PayloadScriptId",
                table: "Attacks",
                newName: "IX_Attacks_PayloadScriptId");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_PayloadPageId",
                table: "Attacks",
                newName: "IX_Attacks_PayloadPageId");

            migrationBuilder.RenameIndex(
                name: "IX_PageKeys_EmailTargetId",
                table: "Attacks",
                newName: "IX_Attacks_EmailTargetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attacks",
                table: "Attacks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_EmailTargets_EmailTargetId",
                table: "Attacks",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_PayloadPages_PayloadPageId",
                table: "Attacks",
                column: "PayloadPageId",
                principalTable: "PayloadPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_PayloadScripts_PayloadScriptId",
                table: "Attacks",
                column: "PayloadScriptId",
                principalTable: "PayloadScripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_EmailTargets_EmailTargetId",
                table: "Attacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_PayloadPages_PayloadPageId",
                table: "Attacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_PayloadScripts_PayloadScriptId",
                table: "Attacks");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attacks",
                table: "Attacks");

            migrationBuilder.RenameTable(
                name: "Attacks",
                newName: "PageKeys");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_Value",
                table: "PageKeys",
                newName: "IX_PageKeys_Value");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_PayloadScriptId",
                table: "PageKeys",
                newName: "IX_PageKeys_PayloadScriptId");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_PayloadPageId",
                table: "PageKeys",
                newName: "IX_PageKeys_PayloadPageId");

            migrationBuilder.RenameIndex(
                name: "IX_Attacks_EmailTargetId",
                table: "PageKeys",
                newName: "IX_PageKeys_EmailTargetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageKeys",
                table: "PageKeys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataDumps_PageKeys_PageKeyId",
                table: "DataDumps",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_PageKeys_PageKeyId",
                table: "EmailMessages",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageKeys_EmailTargets_EmailTargetId",
                table: "PageKeys",
                column: "EmailTargetId",
                principalTable: "EmailTargets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageKeys_PayloadPages_PayloadPageId",
                table: "PageKeys",
                column: "PayloadPageId",
                principalTable: "PayloadPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageKeys_PayloadScripts_PayloadScriptId",
                table: "PageKeys",
                column: "PayloadScriptId",
                principalTable: "PayloadScripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageVisits_PageKeys_PageKeyId",
                table: "PageVisits",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptVisits_PageKeys_PageKeyId",
                table: "ScriptVisits",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetInstructions_PageKeys_PageKeyId",
                table: "TargetInstructions",
                column: "PageKeyId",
                principalTable: "PageKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
