using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ministries",
                columns: new[] { "MinistryId", "MinistryEmail", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "ministry@nsp.com", "Ministry", new byte[] { 130, 16, 99, 84, 111, 254, 136, 23, 139, 157, 53, 247, 17, 126, 19, 58, 194, 230, 233, 56, 218, 141, 79, 36, 231, 16, 96, 188, 157, 35, 192, 147, 149, 81, 83, 193, 130, 195, 20, 193, 88, 224, 64, 166, 149, 56, 17, 220, 149, 204, 202, 97, 55, 176, 120, 232, 85, 113, 65, 8, 167, 199, 106, 28 }, new byte[] { 116, 156, 222, 38, 110, 137, 76, 88, 233, 45, 118, 230, 239, 204, 155, 120, 44, 167, 171, 44, 119, 194, 109, 220, 12, 138, 221, 100, 180, 245, 241, 6, 180, 32, 124, 57, 145, 95, 199, 201, 78, 43, 203, 180, 150, 166, 17, 120, 140, 207, 150, 222, 55, 116, 223, 114, 86, 133, 44, 171, 60, 152, 13, 31, 5, 35, 167, 215, 32, 195, 79, 158, 35, 167, 84, 79, 118, 22, 150, 191, 76, 118, 123, 30, 113, 23, 78, 63, 159, 13, 100, 81, 93, 40, 149, 251, 193, 162, 99, 31, 150, 137, 173, 57, 149, 8, 252, 240, 94, 203, 46, 168, 68, 133, 147, 169, 137, 207, 209, 107, 177, 93, 164, 29, 125, 193, 167, 72 } });

            migrationBuilder.InsertData(
                table: "NodalOfficers",
                columns: new[] { "OfficerId", "OfficerEmail", "OfficerName", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "officer@nsp.com", "Officer", new byte[] { 188, 97, 234, 172, 24, 171, 225, 41, 188, 216, 143, 224, 235, 55, 182, 49, 189, 82, 75, 65, 226, 154, 209, 66, 26, 76, 216, 70, 97, 7, 73, 207, 61, 116, 32, 132, 232, 71, 87, 95, 61, 234, 51, 19, 11, 26, 124, 159, 176, 199, 17, 245, 209, 133, 25, 127, 145, 206, 197, 209, 49, 114, 10, 62 }, new byte[] { 196, 85, 173, 121, 50, 94, 72, 121, 157, 196, 171, 213, 20, 59, 213, 73, 189, 62, 234, 2, 75, 226, 35, 200, 87, 249, 92, 90, 117, 161, 65, 113, 11, 32, 86, 125, 119, 249, 193, 183, 11, 80, 214, 6, 10, 82, 187, 166, 153, 48, 115, 96, 240, 194, 186, 163, 212, 17, 220, 102, 95, 142, 227, 2, 210, 121, 234, 44, 230, 177, 101, 88, 5, 199, 170, 195, 76, 127, 33, 49, 95, 201, 200, 204, 217, 141, 237, 195, 11, 231, 171, 220, 150, 51, 230, 249, 149, 221, 112, 250, 136, 150, 203, 124, 157, 118, 5, 87, 4, 248, 193, 226, 3, 17, 13, 119, 245, 137, 55, 81, 157, 106, 253, 81, 87, 66, 226, 143 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "MinistryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NodalOfficers",
                keyColumn: "OfficerId",
                keyValue: 1);
        }
    }
}
