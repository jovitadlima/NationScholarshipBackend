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
                    AddressPincode = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentCourse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentCourseYear = table.Column<int>(type: "int", nullable: false),
                    ModeOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityBoardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousCourse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousPassingYear = table.Column<int>(type: "int", nullable: false),
                    PreviousClassPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RollNo10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardName10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassingYear10 = table.Column<int>(type: "int", nullable: false),
                    Percentage10 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RollNo12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardName12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassingYear12 = table.Column<int>(type: "int", nullable: false),
                    Percentage12 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddmissionFee = table.Column<int>(type: "int", nullable: false),
                    TutionFee = table.Column<int>(type: "int", nullable: false),
                    OtherFee = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfDisability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentageDisability = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    CertificateUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    _10thMarkSheet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _12thMarkSheet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SchemeId = table.Column<int>(type: "int", nullable: false)
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
                values: new object[] { 1, "ministry@nsp.com", "Ministry", new byte[] { 117, 28, 197, 232, 63, 111, 70, 221, 248, 248, 104, 118, 159, 254, 216, 76, 174, 197, 180, 224, 65, 233, 68, 59, 177, 123, 26, 45, 175, 122, 0, 202, 79, 27, 114, 152, 148, 166, 130, 43, 55, 168, 85, 243, 24, 100, 62, 98, 235, 13, 110, 96, 155, 157, 230, 203, 179, 161, 113, 180, 53, 40, 103, 26 }, new byte[] { 170, 94, 193, 71, 179, 92, 219, 193, 229, 242, 12, 93, 181, 243, 164, 75, 240, 172, 230, 96, 27, 234, 10, 51, 86, 53, 95, 238, 138, 244, 55, 121, 9, 184, 71, 123, 208, 113, 14, 62, 92, 43, 6, 171, 92, 196, 253, 1, 137, 51, 173, 12, 0, 4, 77, 145, 113, 219, 47, 21, 28, 179, 233, 142, 244, 116, 163, 125, 195, 71, 157, 164, 240, 249, 246, 75, 150, 22, 101, 129, 45, 173, 220, 44, 226, 242, 53, 134, 82, 1, 184, 205, 92, 78, 106, 137, 237, 190, 217, 122, 212, 90, 59, 2, 153, 127, 43, 195, 223, 64, 225, 106, 31, 139, 19, 60, 36, 80, 39, 245, 103, 233, 99, 110, 43, 98, 61, 20 } });

            migrationBuilder.InsertData(
                table: "NodalOfficers",
                columns: new[] { "OfficerId", "OfficerEmail", "OfficerName", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "officer@nsp.com", "Officer", new byte[] { 15, 255, 122, 79, 71, 153, 99, 51, 26, 186, 244, 143, 160, 5, 25, 184, 8, 27, 132, 155, 46, 80, 214, 139, 198, 132, 43, 143, 193, 50, 205, 17, 154, 222, 1, 54, 197, 49, 247, 167, 107, 64, 128, 215, 245, 61, 157, 147, 148, 200, 250, 36, 119, 253, 199, 217, 200, 162, 249, 17, 15, 159, 74, 128 }, new byte[] { 82, 198, 153, 101, 101, 130, 133, 201, 210, 134, 11, 83, 222, 37, 47, 155, 0, 191, 45, 206, 169, 250, 186, 193, 102, 168, 254, 34, 156, 186, 51, 31, 235, 239, 161, 180, 169, 203, 212, 121, 200, 16, 163, 52, 184, 37, 152, 113, 112, 71, 183, 202, 40, 248, 48, 77, 204, 120, 202, 162, 150, 37, 113, 27, 42, 206, 45, 120, 107, 146, 223, 198, 74, 18, 216, 133, 231, 139, 251, 121, 111, 69, 221, 136, 18, 42, 246, 202, 155, 4, 208, 3, 149, 38, 113, 92, 123, 181, 199, 92, 5, 98, 32, 4, 201, 20, 26, 5, 116, 191, 181, 129, 47, 60, 145, 177, 53, 155, 69, 217, 126, 4, 240, 71, 85, 32, 142, 20 } });

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
                column: "StudentId",
                unique: true);

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
