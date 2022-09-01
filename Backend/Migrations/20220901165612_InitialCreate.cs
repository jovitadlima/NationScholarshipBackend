using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutes",
                columns: table => new
                {
                    InstituteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstituteCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DiseCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffliatedUniversityState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffliatedUniversityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfAddmission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressPincode = table.Column<int>(type: "int", nullable: false),
                    PrincipalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedByOfficer = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByMinistry = table.Column<bool>(type: "bit", nullable: false),
                    IsRejected = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityAffliationCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutes", x => x.InstituteId);
                });

            migrationBuilder.CreateTable(
                name: "Ministries",
                columns: table => new
                {
                    MinistryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinistryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ministries", x => x.MinistryId);
                });

            migrationBuilder.CreateTable(
                name: "NodalOfficers",
                columns: table => new
                {
                    OfficerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodalOfficers", x => x.OfficerId);
                });

            migrationBuilder.CreateTable(
                name: "ScholarshipSchemes",
                columns: table => new
                {
                    SchemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipSchemes", x => x.SchemeId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StateOfDomicile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AadharNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BankIfscCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    InstituteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Institutes_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "Institutes",
                        principalColumn: "InstituteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScholarshipApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AadharNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Community = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualIncome = table.Column<int>(type: "int", nullable: false),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentCourse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentCourseYear = table.Column<int>(type: "int", nullable: false),
                    ModeOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityBoardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousCourse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousPassingYear = table.Column<int>(type: "int", nullable: false),
                    PreviousClassPercentage = table.Column<int>(type: "int", nullable: false),
                    RollNo10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardName10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassingYear10 = table.Column<int>(type: "int", nullable: false),
                    Percentage10 = table.Column<int>(type: "int", nullable: false),
                    RollNo12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardName12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassingYear12 = table.Column<int>(type: "int", nullable: false),
                    Percentage12 = table.Column<int>(type: "int", nullable: false),
                    AddmissionFee = table.Column<int>(type: "int", nullable: false),
                    TutionFee = table.Column<int>(type: "int", nullable: false),
                    OtherFee = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfDisability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentageDisability = table.Column<int>(type: "int", nullable: false),
                    MartialStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentProfession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincode = table.Column<int>(type: "int", nullable: false),
                    ApprovedByInstitute = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByOfficer = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByMinistry = table.Column<bool>(type: "bit", nullable: false),
                    InstituteCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRejected = table.Column<bool>(type: "bit", nullable: false),
                    DomicileCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteIdCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CasteOrIncomeCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousYearMarksheet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeReceiptOfCurrentYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankPassBook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AadharCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkSheet10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkSheet12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemeId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipApplications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_ScholarshipApplications_ScholarshipSchemes_SchemeId",
                        column: x => x.SchemeId,
                        principalTable: "ScholarshipSchemes",
                        principalColumn: "SchemeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScholarshipApplications_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ministries",
                columns: new[] { "MinistryId", "MinistryEmail", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "ministry@nsp.com", "Ministry", new byte[] { 196, 196, 210, 25, 134, 31, 198, 50, 197, 255, 26, 188, 159, 225, 152, 181, 224, 233, 57, 23, 192, 41, 240, 242, 154, 65, 46, 83, 93, 52, 160, 105, 108, 94, 231, 201, 158, 146, 219, 239, 130, 63, 194, 167, 210, 132, 79, 151, 249, 234, 207, 1, 173, 148, 110, 84, 149, 51, 175, 96, 153, 226, 239, 11 }, new byte[] { 195, 147, 239, 64, 216, 226, 202, 136, 81, 171, 239, 37, 47, 126, 141, 1, 7, 17, 122, 234, 7, 171, 35, 182, 38, 24, 190, 15, 124, 102, 157, 46, 165, 144, 84, 51, 103, 165, 30, 46, 55, 70, 200, 126, 126, 94, 242, 254, 122, 52, 78, 81, 140, 21, 84, 145, 68, 46, 219, 248, 96, 90, 193, 119, 74, 134, 174, 150, 47, 162, 65, 28, 187, 93, 164, 125, 172, 20, 144, 41, 180, 204, 248, 141, 164, 242, 172, 237, 64, 181, 181, 19, 90, 229, 251, 248, 207, 31, 126, 104, 18, 4, 194, 252, 122, 253, 9, 92, 101, 11, 244, 41, 143, 253, 198, 216, 198, 4, 143, 100, 81, 95, 8, 223, 51, 111, 210, 135 } });

            migrationBuilder.InsertData(
                table: "NodalOfficers",
                columns: new[] { "OfficerId", "OfficerEmail", "OfficerName", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "officer@nsp.com", "Officer", new byte[] { 179, 46, 117, 229, 18, 228, 48, 91, 255, 75, 84, 163, 110, 93, 131, 5, 247, 12, 122, 239, 49, 86, 112, 169, 44, 108, 207, 59, 182, 153, 86, 252, 227, 78, 66, 71, 163, 255, 206, 185, 105, 188, 136, 184, 243, 174, 53, 74, 88, 0, 229, 156, 212, 248, 67, 54, 143, 155, 117, 204, 213, 61, 69, 244 }, new byte[] { 196, 113, 219, 23, 51, 234, 191, 34, 119, 205, 225, 69, 12, 2, 188, 182, 247, 1, 45, 56, 168, 96, 110, 216, 212, 204, 47, 121, 142, 161, 58, 140, 213, 31, 102, 0, 134, 65, 37, 229, 33, 69, 2, 103, 93, 83, 164, 200, 41, 197, 205, 100, 147, 173, 202, 211, 228, 175, 67, 247, 196, 222, 113, 246, 28, 112, 202, 182, 49, 205, 111, 188, 100, 49, 18, 69, 27, 240, 129, 2, 126, 75, 75, 63, 1, 131, 64, 159, 43, 89, 218, 115, 157, 126, 189, 184, 81, 52, 127, 153, 20, 152, 51, 214, 81, 81, 115, 250, 198, 14, 227, 137, 231, 187, 121, 55, 10, 191, 21, 95, 225, 158, 28, 73, 12, 98, 124, 175 } });

            migrationBuilder.CreateIndex(
                name: "IX_Institutes_DiseCode_InstituteCode",
                table: "Institutes",
                columns: new[] { "DiseCode", "InstituteCode" },
                unique: true,
                filter: "[DiseCode] IS NOT NULL AND [InstituteCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarshipApplications_SchemeId",
                table: "ScholarshipApplications",
                column: "SchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarshipApplications_StudentId",
                table: "ScholarshipApplications",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AadharNumber_PhoneNo_BankAccountNumber",
                table: "Students",
                columns: new[] { "AadharNumber", "PhoneNo", "BankAccountNumber" },
                unique: true,
                filter: "[AadharNumber] IS NOT NULL AND [PhoneNo] IS NOT NULL AND [BankAccountNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InstituteId",
                table: "Students",
                column: "InstituteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ministries");

            migrationBuilder.DropTable(
                name: "NodalOfficers");

            migrationBuilder.DropTable(
                name: "ScholarshipApplications");

            migrationBuilder.DropTable(
                name: "ScholarshipSchemes");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Institutes");
        }
    }
}
