using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _1640WebDevUMC.Migrations
{
    /// <inheritdoc />
    public partial class V : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    FacultyID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    AcademicYearID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalClosureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacultyID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.AcademicYearID);
                    table.ForeignKey(
                        name: "FK_AcademicYears_Faculties_FacultyID",
                        column: x => x.FacultyID,
                        principalTable: "Faculties",
                        principalColumn: "FacultyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacultyName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfContributions = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Faculties_FacultyName",
                        column: x => x.FacultyName,
                        principalTable: "Faculties",
                        principalColumn: "FacultyID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contributions",
                columns: table => new
                {
                    ContributionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicYearID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributions", x => x.ContributionID);
                    table.ForeignKey(
                        name: "FK_Contributions_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contributions_AspNetUsers_Email",
                        column: x => x.Email,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContributionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_Files_Contributions_ContributionID",
                        column: x => x.ContributionID,
                        principalTable: "Contributions",
                        principalColumn: "ContributionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContributionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Contributions_ContributionID",
                        column: x => x.ContributionID,
                        principalTable: "Contributions",
                        principalColumn: "ContributionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    FileID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContributionID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_Email",
                        column: x => x.Email,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Contributions_ContributionID",
                        column: x => x.ContributionID,
                        principalTable: "Contributions",
                        principalColumn: "ContributionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Files_FileID",
                        column: x => x.FileID,
                        principalTable: "Files",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a29fc34-cf49-477d-8e45-0199d8a8ca14", null, "Marketing Coordinator", "MARKETING COORDINATOR" },
                    { "0bd06470-6120-4334-86c5-2d1fcd75f514", null, "Guest", "GUEST" },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", null, "Student", "STUDENT" },
                    { "cb316555-3aec-4234-aea9-1b4e22add4fb", null, "Admin", "ADMIN" },
                    { "ec5e815d-2ae2-4a7d-a687-fc7502e05560", null, "Marketing Managerment", "MARKETING MANAGERMENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedTime", "Email", "EmailConfirmed", "FacultyName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "NumberOfContributions", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "03bc6e64-e6bc-4bfb-93ec-d8380795c88d", 0, "266f40e1-1c1e-4172-9804-d715cf77848d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarketingManagerment@gmail.com", true, null, false, null, "MARKETINGMANAGERMENT@GMAIL.COM", "MARKETINGMANAGERMENT@GMAIL.COM", 0, "AQAAAAIAAYagAAAAENTiXM2ltFsSFDX0sjUqbC/wWNB1ODDl/MvgaelowVco1YlgccPz/nd41Gdphyb1Kg==", null, false, "VNOO7SFU6U733OQYSFORZPA3CCYCTQZS", false, "MarketingManagerment@gmail.com" },
                    { "216ca0d2-6672-4cc9-8760-752f58c9d318", 0, "77f6fc2b-fc5b-42cf-b773-ed710778d8f6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarketingCoodinator@gmail.com", true, null, false, null, "MARKETINGCOODINATOR@GMAIL.COM", "MARKETINGCOODINATOR@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEPy2FFy18VUAIyausYLfWaeEC+6ELFbcdPO4ka2uLvzMs0vXElcDEY/GjBsa2LxY6A==", null, false, "X6LBPU6MHZ67CDKRT3SGQABIYKKYETTA", false, "MarketingCoodinator@gmail.com" },
                    { "2f3c0a4c-c42e-44e3-b08f-fd446ada9e4b", 0, "89e42fa8-e776-4138-bb59-ab5e114690eb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "student4@gmail.com", true, null, false, null, "STUDENT4@GMAIL.COM", "STUDENT4@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEF/3/wUBT9NOgzMP7Z9XY6oSDH7okOb8W1LQwc11/HC+EPzZ2iqty2mbmQiNver6vQ==", null, false, "QECAUXQTD26FV5XKZAGN6YO4OQRPB6XR", false, "student4@gmail.com" },
                    { "3ee3094c-e0ce-44d8-8746-e28245b63d7b", 0, "cabf1014-7e04-4a49-bac7-014e235628c6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "student8@gmail.com", true, null, false, null, "STUDENT8@GMAIL.COM", "STUDENT8@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEM9Ijsnz7H+/DJbWWWzVmFaZLRf6VoY970Gb4cxfTgbZ+YqntRyPdURiZGz7oSYQZw==", null, false, "3VF7XMHZ2WO6NCFQCCTWOWXCVT7U33NV", false, "student8@gmail.com" },
                    { "47d2b6a5-9b9c-4703-9d06-7505ac14d08f", 0, "a8ea837c-34e3-4c06-95b5-0b8b0cf75ee0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarketingCoodinator1@gmail.com", true, null, false, null, "MARKETINGCOODINATOR1@GMAIL.COM", "MARKETINGCOODINATOR1@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEG+b/+1sW/W6N2fhLzqSdHA9Ar00EQkhEcCbR66pGJG1ac8R3ladiMMEflGI86AK8g==", null, false, "OEV77TI2UFUTNBO74U33KYEPMBIJPJYX", false, "MarketingCoodinator1@gmail.com" },
                    { "54bba5fb-57f9-49f5-9d92-5a06eedb8fc4", 0, "224b0c6e-cd81-4ab2-ae3f-fdcbfddd258e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "student5@gmail.com", true, null, false, null, "STUDENT5@GMAIL.COM", "STUDENT5@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEN2DkDMlhW2AWxuSl+DPXiifpRpBbEQx7GYEwrjsCuQvZL+rIYR3I9kPcD4jf1gumg==", null, false, "T27XLM3SSZNLKJEJQZOBYE5EY5PB2QZD", false, "student5@gmail.com" },
                    { "8b3b94cf-0a9d-4e1b-a3fd-263a6474ada2", 0, "dc2eb935-ed26-4ac0-825c-76c5e4cde5fb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "student9@gmail.com", true, null, false, null, "STUDENT9@GMAIL.COM", "STUDENT9@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEOu+jaX31jxxTTwAZHglNHQrpURqatIjB8jpO+AH0g1OftXGFwanZwrj2vmCAdbBBQ==", null, false, "RWAIPM2JLLYWSSQNKCZ6OKRKRMLVQFIJ", false, "student9@gmail.com" },
                    { "90316e26-012d-4c0c-939c-2dee25cf8183", 0, "ec30a8dd-3a71-4ff0-bbab-f9409cf36d48", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student1@gmail.com", true, null, false, null, "STUDENT1@GMAIL.COM", "STUDENT1@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEKDejowos2MamEJWU+9ieQdEmHvzZMfezMkE1TfEUknawtfZ9WrWI7A7PQ/XLI1+Tg==", null, false, "5KILRK7QTV6GWCSXGNTNRCSQAHD2MCFV", false, "Student1@gmail.com" },
                    { "92de70df-e818-41d9-9d24-4782eb838fe8", 0, "749850b3-0b12-4d39-9682-7469bf66658d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MarketingManagerment1@gmail.com", true, null, false, null, "MARKETINGMANAGERMENT1@GMAIL.COM", "MARKETINGMANAGERMENT1@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEHzq4s10lDXIFitL95HxIswckADNGO0SfULZIq04hzvQDdst0KvHrPt6Gfn8liwF7A==", null, false, "7P4MNUPRM53XPCKTRIUXVDMXNYEXZHLG", false, "MarketingManagerment1@gmail.com" },
                    { "a94809be-838f-4f8a-bccc-003f5f6025ec", 0, "d7e504e8-0099-4861-9194-c58c338f539c", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin@gmail.com", true, null, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEPPDS07tfI8yMRn0KWAt1BEIhlaXdIjBsUm+B8eeQcge5DDWNBtVpODMtLxP09ZB8g==", null, false, "YQIY43F2NSBTAZ2ZAHBAXJ6H5KBHEPZZ", false, "Admin@gmail.com" },
                    { "b7244936-fb02-432a-8bc6-e27d4a8a9ab0", 0, "c6b3372f-eb1d-4e2d-bcd7-1852eff09a2d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "student6@gmail.com", true, null, false, null, "STUDENT6@GMAIL.COM", "STUDENT6@GMAIL.COM", 0, "AQAAAAIAAYagAAAAENLVgxNB7Du0qI4+4Wbd/d9vNbxRCWRlhwM6jzMnJnmnvn7/rfwMBtEG/4nSjK7PGA==", null, false, "CWS5AFJYTDC4C3EN3ESLHSB7BETJALKF", false, "student6@gmail.com" },
                    { "c2512fdf-cab0-423e-87a4-2d33ab8a5a74", 0, "7d5bbb78-7d32-4867-9296-bbbe201bd491", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest1@gmail.com", true, null, false, null, "GUEST1@GMAIL.COM", "GUEST1@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEOtce99JMkRpyi6jWF7KFfhmbQxvjzMDX1Eiu/mxx7gTw4P5SJJn+9A5XAhoQdrnkw==", null, false, "OZUAZZN2PSOSV3UO5CC6YDFB433CBFDF", false, "Guest1@gmail.com" },
                    { "d67405f3-aecc-4873-a19e-ced3fcbeac1d", 0, "e258c7e1-5d38-4a40-962b-a25aefd3a5c2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "student2@gmail.com", true, null, false, null, "STUDENT2@GMAIL.COM", "STUDENT2@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEObW6aLAhR0p4ngWx+NiXiAoWSIpXUUP99JTB1LQQNmjXxAFoAi6/xyhI3RkqEvDvw==", null, false, "SLZUSVP34PBUKPVY63UFZX2RB7JDFMZN", false, "student2@gmail.com" },
                    { "e0323333-b90c-48ab-b4ca-17cbf63231d5", 0, "a7954e4d-e6dd-49c8-80dd-5903b80fab9f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "student7@gmail.com", true, null, false, null, "STUDENT7@GMAIL.COM", "STUDENT7@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEIlYMV/4VsL1MMlEWtVvr9T0IIAezit7N9NuWcnsGQtd/kXfYx6dFYZLLtqzAh27yg==", null, false, "O5YNXENF2PICVCPCS2IBTAZU7JZZPUXS", false, "student7@gmail.com" },
                    { "e1c5de51-1cf3-41fb-809f-734ca787c863", 0, "ca6e85b9-13d8-45c6-b93e-66e725564fdc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "student3@gmail.com", true, null, false, null, "STUDENT3@GMAIL.COM", "STUDENT3@GMAIL.COM", 0, "AQAAAAIAAYagAAAAECfqYcOiXuHmNJR4H+FUa7VJ5NEotGfsMIozDZViePH1E3HMVx4Be3OCXOWLWdslkA==", null, false, "UJ5GTX2CQD6IDK5XGDSUIVAGXF6UWYE2", false, "student3@gmail.com" },
                    { "e33f9319-c5b6-4e98-893e-565327621838", 0, "6366b2d1-e424-4a06-964a-333eeb8fcf99", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student@gmail.com", true, null, false, null, "STUDENT@GMAIL.COM", "STUDENT@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEAKOaX32P4nURuTPOG+R1de+qQo5Spk5ODUs75m1SOjjxo2JOnJSfcEcVEK10SAUkQ==", null, false, "5RWONCWIC23E6Q53ZHCHZLPUUI456NUB", false, "Student@gmail.com" },
                    { "e56ba4dd-0143-4221-84f3-c34e34e0e14f", 0, "dd8d2571-9ae9-44ea-9af5-1d038dd4cb6a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest@gmail.com", true, null, false, null, "GUEST@GMAIL.COM", "GUEST@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEO7FHWTjZ+Mnk1IXD2EDF8/cRY+OK9NAX+V3DafLtsEHN+Put1O+/Su81iE94jN3Ig==", null, false, "724ZAKU2VBDDWZEQLCGCJQ3WJG3P6LLE", false, "Guest@gmail.com" },
                    { "f5719d35-67c3-4458-8b3f-dc5c410bee91", 0, "f3d35431-eef4-49fa-a6be-bf39886c5f33", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin1@gmail.com", true, null, false, null, "ADMIN1@GMAIL.COM", "ADMIN1@GMAIL.COM", 0, "AQAAAAIAAYagAAAAEM3HvIpr8pPqbVL/iJGPpZ3is5dw5O2C3TAra0IOzGVST/UR6Qn27Ai7DJSOTZKP8w==", null, false, "BQ6KJD5FXTGZPPDW7IOMSAJVIWVWDOJI", false, "Admin1@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyID", "CodeSubject", "FacultyName" },
                values: new object[,]
                {
                    { "BUSI1632", "BUSI1632", "Business" },
                    { "BUSI1633", "BUSI1633", "Business" },
                    { "BUSI1702", "BUSI1702", "Business" },
                    { "COMP1640", "COMP1640", "IT" },
                    { "COMP1770", "COMP1770", "IT" },
                    { "COMP1787", "COMP1787", "IT" },
                    { "DESI1219", "DESI1219", "Graphic Design" },
                    { "DESI1237", "DESI1237", "Graphic Design" },
                    { "DESI1240", "DESI1240", "Graphic Design" }
                });

            migrationBuilder.InsertData(
                table: "AcademicYears",
                columns: new[] { "AcademicYearID", "ClosureDate", "FacultyID", "FinalClosureDate", "UploadDate" },
                values: new object[,]
                {
                    { "BUSI1632", new DateTime(2024, 5, 1, 0, 35, 0, 0, DateTimeKind.Unspecified), "BUSI1632", new DateTime(2024, 5, 15, 0, 35, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 24, 0, 35, 0, 0, DateTimeKind.Unspecified) },
                    { "BUSI1633", new DateTime(2024, 5, 2, 0, 35, 0, 0, DateTimeKind.Unspecified), "BUSI1633", new DateTime(2024, 5, 16, 0, 35, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 25, 0, 35, 0, 0, DateTimeKind.Unspecified) },
                    { "BUSI1702", new DateTime(2024, 5, 4, 0, 36, 0, 0, DateTimeKind.Unspecified), "BUSI1702", new DateTime(2024, 5, 18, 0, 36, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 27, 0, 36, 0, 0, DateTimeKind.Unspecified) },
                    { "COMP1640", new DateTime(2024, 5, 22, 0, 37, 0, 0, DateTimeKind.Unspecified), "COMP1640", new DateTime(2024, 6, 5, 0, 37, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 15, 0, 37, 0, 0, DateTimeKind.Unspecified) },
                    { "COMP1770", new DateTime(2024, 5, 18, 0, 37, 0, 0, DateTimeKind.Unspecified), "COMP1770", new DateTime(2024, 6, 1, 0, 37, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 11, 0, 37, 0, 0, DateTimeKind.Unspecified) },
                    { "COMP1787", new DateTime(2024, 5, 11, 0, 37, 0, 0, DateTimeKind.Unspecified), "COMP1787", new DateTime(2024, 5, 25, 0, 37, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 4, 0, 37, 0, 0, DateTimeKind.Unspecified) },
                    { "DESI1219", new DateTime(2024, 5, 5, 0, 36, 0, 0, DateTimeKind.Unspecified), "DESI1219", new DateTime(2024, 5, 19, 0, 36, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 28, 0, 36, 0, 0, DateTimeKind.Unspecified) },
                    { "DESI1237", new DateTime(2024, 5, 8, 0, 36, 0, 0, DateTimeKind.Unspecified), "DESI1237", new DateTime(2024, 5, 22, 0, 37, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 1, 0, 36, 0, 0, DateTimeKind.Unspecified) },
                    { "DESI1240", new DateTime(2024, 5, 6, 0, 36, 0, 0, DateTimeKind.Unspecified), "DESI1240", new DateTime(2024, 5, 20, 0, 36, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 29, 0, 36, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "ApplicationUserId" },
                values: new object[,]
                {
                    { "ec5e815d-2ae2-4a7d-a687-fc7502e05560", "03bc6e64-e6bc-4bfb-93ec-d8380795c88d", null },
                    { "0a29fc34-cf49-477d-8e45-0199d8a8ca14", "216ca0d2-6672-4cc9-8760-752f58c9d318", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "2f3c0a4c-c42e-44e3-b08f-fd446ada9e4b", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "3ee3094c-e0ce-44d8-8746-e28245b63d7b", null },
                    { "0a29fc34-cf49-477d-8e45-0199d8a8ca14", "47d2b6a5-9b9c-4703-9d06-7505ac14d08f", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "54bba5fb-57f9-49f5-9d92-5a06eedb8fc4", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "8b3b94cf-0a9d-4e1b-a3fd-263a6474ada2", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "90316e26-012d-4c0c-939c-2dee25cf8183", null },
                    { "ec5e815d-2ae2-4a7d-a687-fc7502e05560", "92de70df-e818-41d9-9d24-4782eb838fe8", null },
                    { "cb316555-3aec-4234-aea9-1b4e22add4fb", "a94809be-838f-4f8a-bccc-003f5f6025ec", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "b7244936-fb02-432a-8bc6-e27d4a8a9ab0", null },
                    { "0bd06470-6120-4334-86c5-2d1fcd75f514", "c2512fdf-cab0-423e-87a4-2d33ab8a5a74", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "d67405f3-aecc-4873-a19e-ced3fcbeac1d", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "e0323333-b90c-48ab-b4ca-17cbf63231d5", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "e1c5de51-1cf3-41fb-809f-734ca787c863", null },
                    { "c0c2362f-3809-4fc2-b3b8-4bf29d3d41f2", "e33f9319-c5b6-4e98-893e-565327621838", null },
                    { "0bd06470-6120-4334-86c5-2d1fcd75f514", "e56ba4dd-0143-4221-84f3-c34e34e0e14f", null },
                    { "cb316555-3aec-4234-aea9-1b4e22add4fb", "f5719d35-67c3-4458-8b3f-dc5c410bee91", null }
                });

            migrationBuilder.InsertData(
                table: "Contributions",
                columns: new[] { "ContributionID", "AcademicYearID", "Content", "Email", "IsPublic", "StudentEmail", "Title", "UploadTime" },
                values: new object[,]
                {
                    { "Design Research Project", "DESI1219", "Create plans and methods to collect and analyze information to shape and develop new products, services or experiences.", "216ca0d2-6672-4cc9-8760-752f58c9d318", false, "", " Design Research Project ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Enterprise Web Software Development", "COMP1640", "Building a secure web-enabled role-based system to collect innovative ideas from staff in a large University.", "216ca0d2-6672-4cc9-8760-752f58c9d318", false, "", "Enterprise Web Software Development", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Interdisciplinary Design", "DESI1237", "combines methods, knowledge and resources from many different fields to create innovative solutions to complex challenges.", "03bc6e64-e6bc-4bfb-93ec-d8380795c88d", false, "", "Interdisciplinary Design", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Negotiations", "BUSI1632", "The process of negotiating between parties to reach an agreement or resolve a problem in a harmonious manner that benefits all parties involved.", "216ca0d2-6672-4cc9-8760-752f58c9d318", false, "", "Negotiations", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Organisational Decision Making", "BUSI1702", "The process of choosing between options to achieve specific organizational goals, based on information, solutions, and internal and external factors.", "03bc6e64-e6bc-4bfb-93ec-d8380795c88d", false, "", "Organisational Decision Making", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Professional Practice & Portfolio ", "DESI1240", "Build and manage a collection of projects, skills and personal achievements to demonstrate competence and experience in the area of expertise.", "03bc6e64-e6bc-4bfb-93ec-d8380795c88d", false, "", "Professional Practice & Portfolio ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Professional Project Management ", "COMP1770", "Organize and direct resources to achieve specific project goals.", "216ca0d2-6672-4cc9-8760-752f58c9d318", false, "", "Professional Project Management ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Requirements Management", "COMP1787", "Identify, monitor and control project requirements to ensure that the product or service developed meets those requirements.", "216ca0d2-6672-4cc9-8760-752f58c9d318", false, "", "Requirements Management", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "Strategy for Managers", "BUSI1633", "Long-term planning to shape and achieve organizational goals through analysis of the organization's environment, resources, and capabilities.", "216ca0d2-6672-4cc9-8760-752f58c9d318", false, "", "Strategy for Managers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYears_FacultyID",
                table: "AcademicYears",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FacultyName",
                table: "AspNetUsers",
                column: "FacultyName");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ContributionID",
                table: "Comments",
                column: "ContributionID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Email",
                table: "Comments",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FileID",
                table: "Comments",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_AcademicYearID",
                table: "Contributions",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_Email",
                table: "Contributions",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ContributionID",
                table: "Files",
                column: "ContributionID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ApplicationUserId",
                table: "Notifications",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContributionID",
                table: "Notifications",
                column: "ContributionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Contributions");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
