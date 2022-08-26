using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class changedFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstituteId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_InstituteId",
                table: "Students",
                column: "InstituteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "InstituteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_InstituteId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InstituteId",
                table: "Students");
        }
    }
}
