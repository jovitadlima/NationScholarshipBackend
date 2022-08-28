using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class Auth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Students");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "NodalOfficers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "NodalOfficers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Ministries",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Ministries",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Institutes",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Institutes",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "NodalOfficers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "NodalOfficers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Ministries");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Ministries");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Institutes");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
