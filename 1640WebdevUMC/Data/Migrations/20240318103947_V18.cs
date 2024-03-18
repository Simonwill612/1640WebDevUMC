using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Data.Migrations
{
    /// <inheritdoc />
    public partial class V18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Faculties_FacultyID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_Accounts_AccountsId",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Accounts_AccountsId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AccountsId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_AccountsId",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_FacultyID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountsId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AccountsId",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FacultyID",
                table: "Accounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FacultyID",
                table: "Accounts",
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
                name: "IX_Accounts_FacultyID",
                table: "Accounts",
                column: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Faculties_FacultyID",
                table: "Accounts",
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
                name: "FK_Notifications_Accounts_AccountsId",
                table: "Notifications",
                column: "AccountsId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
