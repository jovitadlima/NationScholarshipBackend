using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutes",
                columns: table => new
                {
                    InstituteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstituteCategory = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteCode = table.Column<int>(type: "int", nullable: false),
                    DiseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffliatedUniversityState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffliatedUniversityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfAddmission = table.Column<int>(type: "int", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressPincode = table.Column<int>(type: "int", nullable: false),
                    PrincipalName = table.Column<int>(type: "int", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedByOfficer = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByMinistery = table.Column<bool>(type: "bit", nullable: false)
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
                    MinistryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    OfficerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    MyProperty = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateOfDomicile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AadharNumber = table.Column<int>(type: "int", nullable: false),
                    BankIfscCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "InstituteDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituteDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_InstituteDocuments_Institutes_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "Institutes",
                        principalColumn: "InstituteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScholarshipApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AadharNumber = table.Column<int>(type: "int", nullable: false),
                    Community = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualIncome = table.Column<double>(type: "float", nullable: false),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentCourse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentCourseYear = table.Column<int>(type: "int", nullable: false),
                    ModeOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    MyProperty = table.Column<int>(type: "int", nullable: false),
                    ApprovedByInstitute = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByOfficer = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByMinistry = table.Column<bool>(type: "bit", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SchemeId = table.Column<int>(type: "int", nullable: false),
                    ScholarshipSchemeSchemeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipApplications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_ScholarshipApplications_ScholarshipSchemes_ScholarshipSchemeSchemeId",
                        column: x => x.ScholarshipSchemeSchemeId,
                        principalTable: "ScholarshipSchemes",
                        principalColumn: "SchemeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScholarshipApplications_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentDocuments",
                columns: table => new
                {
                    DocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ScholarshipApplicationApplicationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDocuments", x => x.DocId);
                    table.ForeignKey(
                        name: "FK_StudentDocuments_ScholarshipApplications_ScholarshipApplicationApplicationId",
                        column: x => x.ScholarshipApplicationApplicationId,
                        principalTable: "ScholarshipApplications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstituteDocuments_InstituteId",
                table: "InstituteDocuments",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarshipApplications_ScholarshipSchemeSchemeId",
                table: "ScholarshipApplications",
                column: "ScholarshipSchemeSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarshipApplications_StudentId",
                table: "ScholarshipApplications",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentDocuments_ScholarshipApplicationApplicationId",
                table: "StudentDocuments",
                column: "ScholarshipApplicationApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstituteDocuments");

            migrationBuilder.DropTable(
                name: "Ministries");

            migrationBuilder.DropTable(
                name: "NodalOfficers");

            migrationBuilder.DropTable(
                name: "StudentDocuments");

            migrationBuilder.DropTable(
                name: "Institutes");

            migrationBuilder.DropTable(
                name: "ScholarshipApplications");

            migrationBuilder.DropTable(
                name: "ScholarshipSchemes");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
