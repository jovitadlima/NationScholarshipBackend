﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ministries",
                keyColumn: "MinistryId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 127, 42, 92, 197, 84, 202, 155, 66, 33, 205, 24, 7, 199, 43, 53, 186, 188, 236, 33, 13, 210, 125, 212, 216, 116, 125, 233, 132, 51, 31, 118, 54, 159, 29, 71, 131, 80, 110, 203, 118, 252, 27, 5, 120, 93, 144, 187, 89, 146, 162, 44, 69, 104, 219, 46, 185, 242, 28, 152, 49, 235, 67, 202, 55 }, new byte[] { 5, 85, 32, 215, 27, 205, 198, 108, 15, 14, 107, 109, 59, 24, 46, 219, 122, 187, 161, 233, 142, 255, 177, 185, 178, 169, 137, 147, 11, 163, 79, 168, 207, 28, 88, 104, 60, 155, 38, 32, 206, 54, 168, 243, 43, 253, 236, 24, 40, 251, 131, 116, 56, 94, 125, 157, 233, 151, 236, 81, 204, 123, 224, 146, 69, 9, 185, 222, 91, 84, 173, 7, 223, 62, 148, 200, 95, 7, 251, 95, 1, 72, 241, 61, 25, 232, 83, 91, 25, 77, 18, 163, 174, 185, 150, 220, 163, 191, 176, 178, 160, 47, 68, 169, 42, 153, 127, 184, 214, 74, 133, 59, 128, 153, 244, 155, 185, 227, 164, 4, 82, 31, 51, 101, 101, 227, 134, 54 } });

            migrationBuilder.UpdateData(
                table: "NodalOfficers",
                keyColumn: "OfficerId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 191, 26, 245, 111, 44, 128, 193, 240, 197, 59, 101, 105, 59, 171, 208, 62, 65, 137, 130, 75, 191, 62, 185, 210, 153, 8, 48, 90, 57, 167, 230, 3, 75, 235, 48, 225, 4, 250, 72, 6, 80, 40, 163, 49, 196, 94, 185, 139, 122, 130, 230, 191, 108, 91, 158, 189, 106, 111, 205, 8, 174, 159, 214, 104 }, new byte[] { 86, 190, 185, 192, 173, 198, 41, 18, 181, 40, 92, 152, 15, 236, 229, 52, 36, 46, 96, 230, 163, 223, 110, 45, 73, 239, 162, 109, 49, 204, 61, 47, 210, 164, 172, 129, 187, 88, 196, 29, 87, 137, 86, 242, 13, 102, 152, 76, 100, 72, 151, 108, 233, 174, 72, 125, 5, 101, 210, 8, 79, 183, 246, 96, 161, 15, 253, 174, 138, 97, 9, 143, 238, 45, 196, 151, 67, 18, 132, 64, 216, 159, 102, 184, 13, 134, 139, 147, 22, 59, 191, 157, 49, 45, 224, 137, 239, 213, 96, 119, 117, 161, 158, 21, 10, 6, 221, 185, 182, 161, 76, 89, 42, 85, 246, 67, 158, 165, 67, 39, 8, 72, 116, 91, 90, 22, 162, 55 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
