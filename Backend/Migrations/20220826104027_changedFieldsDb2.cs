using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class changedFieldsDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "ScholarshipApplications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "ScholarshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
