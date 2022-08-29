using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class change1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_12thMarkSheet",
                table: "ScholarshipApplications",
                newName: "Religion");

            migrationBuilder.RenameColumn(
                name: "_10thMarkSheet",
                table: "ScholarshipApplications",
                newName: "MarkSheet12");

            migrationBuilder.AlterColumn<string>(
                name: "TutionFee",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PreviousPassingYear",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PreviousClassPercentage",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PresentCourseYear",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Pincode",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PercentageDisability",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Percentage12",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Percentage10",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PassingYear12",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PassingYear10",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "OtherFee",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IsDisabled",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "AddmissionFee",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "MarkSheet10",
                table: "ScholarshipApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Ministries",
                keyColumn: "MinistryId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 241, 109, 232, 175, 211, 113, 126, 86, 234, 181, 203, 160, 170, 101, 204, 102, 178, 106, 25, 95, 179, 93, 38, 176, 110, 53, 210, 93, 200, 16, 243, 106, 153, 26, 182, 121, 156, 242, 96, 142, 164, 218, 121, 29, 240, 65, 55, 247, 123, 234, 37, 56, 109, 118, 102, 73, 185, 131, 84, 69, 88, 68, 158, 133 }, new byte[] { 250, 187, 236, 97, 137, 121, 195, 17, 184, 119, 198, 164, 3, 95, 82, 196, 174, 5, 165, 150, 67, 164, 63, 72, 123, 81, 32, 197, 102, 104, 108, 4, 198, 104, 27, 178, 138, 44, 169, 40, 125, 228, 44, 151, 242, 195, 199, 242, 92, 217, 82, 125, 166, 81, 226, 194, 167, 104, 149, 33, 99, 115, 122, 66, 216, 192, 244, 29, 83, 116, 95, 153, 152, 124, 167, 152, 62, 140, 255, 85, 238, 55, 218, 111, 233, 170, 79, 189, 49, 187, 150, 44, 195, 114, 22, 13, 211, 40, 192, 190, 72, 4, 145, 192, 15, 228, 242, 218, 145, 31, 47, 0, 218, 137, 84, 112, 156, 226, 153, 19, 130, 25, 164, 43, 57, 36, 219, 35 } });

            migrationBuilder.UpdateData(
                table: "NodalOfficers",
                keyColumn: "OfficerId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 142, 230, 52, 65, 201, 49, 99, 8, 60, 61, 240, 43, 67, 66, 164, 125, 144, 2, 17, 141, 138, 8, 0, 48, 94, 217, 219, 103, 231, 158, 237, 153, 171, 129, 25, 103, 154, 94, 0, 72, 152, 138, 88, 244, 12, 7, 188, 93, 190, 115, 184, 129, 87, 245, 238, 125, 51, 174, 170, 72, 78, 176, 188, 18 }, new byte[] { 246, 8, 48, 167, 73, 101, 106, 14, 111, 116, 46, 217, 48, 230, 242, 64, 185, 17, 133, 205, 33, 58, 135, 64, 13, 94, 255, 152, 33, 183, 49, 213, 180, 127, 212, 174, 136, 170, 29, 225, 152, 206, 240, 255, 128, 250, 230, 230, 211, 144, 226, 198, 0, 100, 47, 252, 143, 207, 222, 43, 45, 160, 83, 26, 94, 157, 19, 112, 118, 71, 21, 113, 152, 234, 80, 95, 248, 45, 250, 155, 110, 72, 253, 205, 23, 144, 255, 190, 161, 206, 212, 155, 96, 111, 135, 137, 164, 108, 68, 46, 245, 198, 120, 45, 17, 96, 3, 93, 234, 216, 6, 28, 103, 127, 81, 185, 143, 206, 71, 172, 199, 126, 126, 254, 162, 192, 178, 82 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkSheet10",
                table: "ScholarshipApplications");

            migrationBuilder.RenameColumn(
                name: "Religion",
                table: "ScholarshipApplications",
                newName: "_12thMarkSheet");

            migrationBuilder.RenameColumn(
                name: "MarkSheet12",
                table: "ScholarshipApplications",
                newName: "_10thMarkSheet");

            migrationBuilder.AlterColumn<int>(
                name: "TutionFee",
                table: "ScholarshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreviousPassingYear",
                table: "ScholarshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousClassPercentage",
                table: "ScholarshipApplications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PresentCourseYear",
                table: "ScholarshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Pincode",
                table: "ScholarshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentageDisability",
                table: "ScholarshipApplications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage12",
                table: "ScholarshipApplications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage10",
                table: "ScholarshipApplications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PassingYear12",
                table: "ScholarshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PassingYear10",
                table: "ScholarshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OtherFee",
                table: "ScholarshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "ScholarshipApplications",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddmissionFee",
                table: "ScholarshipApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Ministries",
                keyColumn: "MinistryId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 117, 28, 197, 232, 63, 111, 70, 221, 248, 248, 104, 118, 159, 254, 216, 76, 174, 197, 180, 224, 65, 233, 68, 59, 177, 123, 26, 45, 175, 122, 0, 202, 79, 27, 114, 152, 148, 166, 130, 43, 55, 168, 85, 243, 24, 100, 62, 98, 235, 13, 110, 96, 155, 157, 230, 203, 179, 161, 113, 180, 53, 40, 103, 26 }, new byte[] { 170, 94, 193, 71, 179, 92, 219, 193, 229, 242, 12, 93, 181, 243, 164, 75, 240, 172, 230, 96, 27, 234, 10, 51, 86, 53, 95, 238, 138, 244, 55, 121, 9, 184, 71, 123, 208, 113, 14, 62, 92, 43, 6, 171, 92, 196, 253, 1, 137, 51, 173, 12, 0, 4, 77, 145, 113, 219, 47, 21, 28, 179, 233, 142, 244, 116, 163, 125, 195, 71, 157, 164, 240, 249, 246, 75, 150, 22, 101, 129, 45, 173, 220, 44, 226, 242, 53, 134, 82, 1, 184, 205, 92, 78, 106, 137, 237, 190, 217, 122, 212, 90, 59, 2, 153, 127, 43, 195, 223, 64, 225, 106, 31, 139, 19, 60, 36, 80, 39, 245, 103, 233, 99, 110, 43, 98, 61, 20 } });

            migrationBuilder.UpdateData(
                table: "NodalOfficers",
                keyColumn: "OfficerId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 15, 255, 122, 79, 71, 153, 99, 51, 26, 186, 244, 143, 160, 5, 25, 184, 8, 27, 132, 155, 46, 80, 214, 139, 198, 132, 43, 143, 193, 50, 205, 17, 154, 222, 1, 54, 197, 49, 247, 167, 107, 64, 128, 215, 245, 61, 157, 147, 148, 200, 250, 36, 119, 253, 199, 217, 200, 162, 249, 17, 15, 159, 74, 128 }, new byte[] { 82, 198, 153, 101, 101, 130, 133, 201, 210, 134, 11, 83, 222, 37, 47, 155, 0, 191, 45, 206, 169, 250, 186, 193, 102, 168, 254, 34, 156, 186, 51, 31, 235, 239, 161, 180, 169, 203, 212, 121, 200, 16, 163, 52, 184, 37, 152, 113, 112, 71, 183, 202, 40, 248, 48, 77, 204, 120, 202, 162, 150, 37, 113, 27, 42, 206, 45, 120, 107, 146, 223, 198, 74, 18, 216, 133, 231, 139, 251, 121, 111, 69, 221, 136, 18, 42, 246, 202, 155, 4, 208, 3, 149, 38, 113, 92, 123, 181, 199, 92, 5, 98, 32, 4, 201, 20, 26, 5, 116, 191, 181, 129, 47, 60, 145, 177, 53, 155, 69, 217, 126, 4, 240, 71, 85, 32, 142, 20 } });
        }
    }
}
