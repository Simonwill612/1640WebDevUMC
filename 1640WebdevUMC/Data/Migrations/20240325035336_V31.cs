using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Data.Migrations
{
    /// <inheritdoc />
    public partial class V31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYears_Faculties_FacultyId",
                table: "AcademicYears");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AcademicYears_AcademicYearId",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId1",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_DownloadHistories_Contributions_ContributionId",
                table: "DownloadHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Contributions_ContributionId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Contributions_ContributionId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_RecipientUserId1",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Contributions_ContributionId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_AcademicYearId",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_UserId1",
                table: "Contributions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicYears",
                table: "AcademicYears");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AcademicYears");

            migrationBuilder.RenameColumn(
                name: "RecipientUserId",
                table: "Notifications",
                newName: "RecipientUserID");

            migrationBuilder.RenameColumn(
                name: "ContributionId",
                table: "Notifications",
                newName: "ContributionID");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "Notifications",
                newName: "NotificationID");

            migrationBuilder.RenameColumn(
                name: "RecipientUserId1",
                table: "Notifications",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ContributionId",
                table: "Notifications",
                newName: "IX_Notifications_ContributionID");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_RecipientUserId1",
                table: "Notifications",
                newName: "IX_Notifications_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "ContributionId",
                table: "Images",
                newName: "ContributionID");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Images",
                newName: "ImageID");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ContributionId",
                table: "Images",
                newName: "IX_Images_ContributionID");

            migrationBuilder.RenameColumn(
                name: "ContributionId",
                table: "Files",
                newName: "ContributionID");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Files",
                newName: "FileID");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ContributionId",
                table: "Files",
                newName: "IX_Files_ContributionID");

            migrationBuilder.RenameColumn(
                name: "MarketingManagerId",
                table: "DownloadHistories",
                newName: "MarketingManagerID");

            migrationBuilder.RenameColumn(
                name: "ContributionId",
                table: "DownloadHistories",
                newName: "ContributionID");

            migrationBuilder.RenameColumn(
                name: "DownloadId",
                table: "DownloadHistories",
                newName: "DownloadID");

            migrationBuilder.RenameIndex(
                name: "IX_DownloadHistories_ContributionId",
                table: "DownloadHistories",
                newName: "IX_DownloadHistories_ContributionID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Contributions",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ContributionId",
                table: "Contributions",
                newName: "ContributionID");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "AcademicYears",
                newName: "FacultyID");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "AcademicYears",
                newName: "UploadDate");

            migrationBuilder.RenameColumn(
                name: "FinalDate",
                table: "AcademicYears",
                newName: "FinalClosureDate");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYears_FacultyId",
                table: "AcademicYears",
                newName: "IX_AcademicYears_FacultyID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecipientUserID",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NotificationType",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContributionID",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContributionID",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Images",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadDate",
                table: "Files",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "FileContent",
                table: "Files",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContributionID",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FacultyName",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "FacultyID",
                table: "Faculties",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "MarketingManagerID",
                table: "DownloadHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DownloadDate",
                table: "DownloadHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContributionID",
                table: "DownloadHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Contributions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadDate",
                table: "Contributions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "SelectedForPublication",
                table: "Contributions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinalClosureDate",
                table: "Contributions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosureDate",
                table: "Contributions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearID",
                table: "Contributions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "FacultyID",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FacultyID",
                table: "AcademicYears",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "YearID",
                table: "AcademicYears",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "FacultyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicYears",
                table: "AcademicYears",
                column: "YearID");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_UserID",
                table: "Contributions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_YearID",
                table: "Contributions",
                column: "YearID");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYears_Faculties_FacultyID",
                table: "AcademicYears",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyID",
                table: "AspNetUsers",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AcademicYears_YearID",
                table: "Contributions",
                column: "YearID",
                principalTable: "AcademicYears",
                principalColumn: "YearID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AspNetUsers_UserID",
                table: "Contributions",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DownloadHistories_Contributions_ContributionID",
                table: "DownloadHistories",
                column: "ContributionID",
                principalTable: "Contributions",
                principalColumn: "ContributionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Contributions_ContributionID",
                table: "Files",
                column: "ContributionID",
                principalTable: "Contributions",
                principalColumn: "ContributionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Contributions_ContributionID",
                table: "Images",
                column: "ContributionID",
                principalTable: "Contributions",
                principalColumn: "ContributionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ApplicationUserId",
                table: "Notifications",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Contributions_ContributionID",
                table: "Notifications",
                column: "ContributionID",
                principalTable: "Contributions",
                principalColumn: "ContributionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYears_Faculties_FacultyID",
                table: "AcademicYears");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AcademicYears_YearID",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AspNetUsers_UserID",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_DownloadHistories_Contributions_ContributionID",
                table: "DownloadHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Contributions_ContributionID",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Contributions_ContributionID",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ApplicationUserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Contributions_ContributionID",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_UserID",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_YearID",
                table: "Contributions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicYears",
                table: "AcademicYears");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "FacultyID",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "YearID",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "YearID",
                table: "AcademicYears");

            migrationBuilder.RenameColumn(
                name: "RecipientUserID",
                table: "Notifications",
                newName: "RecipientUserId");

            migrationBuilder.RenameColumn(
                name: "ContributionID",
                table: "Notifications",
                newName: "ContributionId");

            migrationBuilder.RenameColumn(
                name: "NotificationID",
                table: "Notifications",
                newName: "NotificationId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Notifications",
                newName: "RecipientUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ContributionID",
                table: "Notifications",
                newName: "IX_Notifications_ContributionId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ApplicationUserId",
                table: "Notifications",
                newName: "IX_Notifications_RecipientUserId1");

            migrationBuilder.RenameColumn(
                name: "ContributionID",
                table: "Images",
                newName: "ContributionId");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "Images",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ContributionID",
                table: "Images",
                newName: "IX_Images_ContributionId");

            migrationBuilder.RenameColumn(
                name: "ContributionID",
                table: "Files",
                newName: "ContributionId");

            migrationBuilder.RenameColumn(
                name: "FileID",
                table: "Files",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ContributionID",
                table: "Files",
                newName: "IX_Files_ContributionId");

            migrationBuilder.RenameColumn(
                name: "MarketingManagerID",
                table: "DownloadHistories",
                newName: "MarketingManagerId");

            migrationBuilder.RenameColumn(
                name: "ContributionID",
                table: "DownloadHistories",
                newName: "ContributionId");

            migrationBuilder.RenameColumn(
                name: "DownloadID",
                table: "DownloadHistories",
                newName: "DownloadId");

            migrationBuilder.RenameIndex(
                name: "IX_DownloadHistories_ContributionID",
                table: "DownloadHistories",
                newName: "IX_DownloadHistories_ContributionId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Contributions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ContributionID",
                table: "Contributions",
                newName: "ContributionId");

            migrationBuilder.RenameColumn(
                name: "FacultyID",
                table: "AcademicYears",
                newName: "FacultyId");

            migrationBuilder.RenameColumn(
                name: "UploadDate",
                table: "AcademicYears",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "FinalClosureDate",
                table: "AcademicYears",
                newName: "FinalDate");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYears_FacultyID",
                table: "AcademicYears",
                newName: "IX_AcademicYears_FacultyId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Notifications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "RecipientUserId",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NotificationType",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ContributionId",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ContributionId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image1",
                table: "Images",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadDate",
                table: "Files",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FileContent",
                table: "Files",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ContributionId",
                table: "Files",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FacultyName",
                table: "Faculties",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Faculties",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "MarketingManagerId",
                table: "DownloadHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DownloadDate",
                table: "DownloadHistories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ContributionId",
                table: "DownloadHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Contributions",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadDate",
                table: "Contributions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "SelectedForPublication",
                table: "Contributions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinalClosureDate",
                table: "Contributions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosureDate",
                table: "Contributions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "AcademicYearId",
                table: "Contributions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                table: "Contributions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Contributions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FacultyID",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FacultyId",
                table: "AcademicYears",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AcademicYears",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicYears",
                table: "AcademicYears",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_AcademicYearId",
                table: "Contributions",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_UserId1",
                table: "Contributions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYears_Faculties_FacultyId",
                table: "AcademicYears",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyID",
                table: "AspNetUsers",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AcademicYears_AcademicYearId",
                table: "Contributions",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId1",
                table: "Contributions",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DownloadHistories_Contributions_ContributionId",
                table: "DownloadHistories",
                column: "ContributionId",
                principalTable: "Contributions",
                principalColumn: "ContributionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Contributions_ContributionId",
                table: "Files",
                column: "ContributionId",
                principalTable: "Contributions",
                principalColumn: "ContributionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Contributions_ContributionId",
                table: "Images",
                column: "ContributionId",
                principalTable: "Contributions",
                principalColumn: "ContributionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_RecipientUserId1",
                table: "Notifications",
                column: "RecipientUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Contributions_ContributionId",
                table: "Notifications",
                column: "ContributionId",
                principalTable: "Contributions",
                principalColumn: "ContributionId");
        }
    }
}
