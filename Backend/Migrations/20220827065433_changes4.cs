using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class changes4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScholarshipApplications_ScholarshipSchemes_ScholarshipSchemeSchemeId",
                table: "ScholarshipApplications");

            migrationBuilder.DropIndex(
                name: "IX_ScholarshipApplications_ScholarshipSchemeSchemeId",
                table: "ScholarshipApplications");

            migrationBuilder.DropColumn(
                name: "ScholarshipSchemeSchemeId",
                table: "ScholarshipApplications");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarshipApplications_SchemeId",
                table: "ScholarshipApplications",
                column: "SchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScholarshipApplications_ScholarshipSchemes_SchemeId",
                table: "ScholarshipApplications",
                column: "SchemeId",
                principalTable: "ScholarshipSchemes",
                principalColumn: "SchemeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScholarshipApplications_ScholarshipSchemes_SchemeId",
                table: "ScholarshipApplications");

            migrationBuilder.DropIndex(
                name: "IX_ScholarshipApplications_SchemeId",
                table: "ScholarshipApplications");

            migrationBuilder.AddColumn<int>(
                name: "ScholarshipSchemeSchemeId",
                table: "ScholarshipApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScholarshipApplications_ScholarshipSchemeSchemeId",
                table: "ScholarshipApplications",
                column: "ScholarshipSchemeSchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScholarshipApplications_ScholarshipSchemes_ScholarshipSchemeSchemeId",
                table: "ScholarshipApplications",
                column: "ScholarshipSchemeSchemeId",
                principalTable: "ScholarshipSchemes",
                principalColumn: "SchemeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
