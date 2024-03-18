using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Data.Migrations
{
    /// <inheritdoc />
    public partial class V15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId1",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_RecipientUserId1",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FacultyID",
                table: "AspNetUsers");

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
                name: "RoleName",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_FacultyId",
                table: "Accounts",
                column: "FacultyId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_Accounts_UserId1",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Accounts_RecipientUserId1",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacultyID",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FacultyID",
                table: "AspNetUsers",
                column: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyID",
                table: "AspNetUsers",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId1",
                table: "Contributions",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_RecipientUserId1",
                table: "Notifications",
                column: "RecipientUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
