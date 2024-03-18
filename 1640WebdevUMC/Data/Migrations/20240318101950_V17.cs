using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Data.Migrations
{
    /// <inheritdoc />
    public partial class V17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_Accounts_UserId1",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Accounts_RecipientUserId1",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "AccountsId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountsId",
                table: "Contributions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacultyID",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AccountsId",
                table: "Notifications",
                column: "AccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_AccountsId",
                table: "Contributions",
                column: "AccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FacultyID",
                table: "AspNetUsers",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyID",
                table: "AspNetUsers",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_Accounts_AccountsId",
                table: "Contributions",
                column: "AccountsId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId1",
                table: "Contributions",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Accounts_AccountsId",
                table: "Notifications",
                column: "AccountsId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_RecipientUserId1",
                table: "Notifications",
                column: "RecipientUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_Accounts_AccountsId",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId1",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Accounts_AccountsId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_RecipientUserId1",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AccountsId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_AccountsId",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FacultyID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "AccountsId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AccountsId",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacultyID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_Accounts_UserId1",
                table: "Contributions",
                column: "UserId1",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Accounts_RecipientUserId1",
                table: "Notifications",
                column: "RecipientUserId1",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
