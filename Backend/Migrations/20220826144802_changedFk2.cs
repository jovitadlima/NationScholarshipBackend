using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class changedFk2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "InstituteId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstituteCode",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstituteCode",
                table: "Institutes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "InstituteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InstituteCode",
                table: "ScholarshipApplications");

            migrationBuilder.AlterColumn<int>(
                name: "InstituteId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InstituteCode",
                table: "Institutes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "InstituteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
