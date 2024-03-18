using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Data.Migrations
{
    /// <inheritdoc />
    public partial class V16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Faculties_FacultyId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Accounts",
                newName: "FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_FacultyId",
                table: "Accounts",
                newName: "IX_Accounts_FacultyID");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Faculties_FacultyID",
                table: "Accounts",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Faculties_FacultyID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "FacultyID",
                table: "Accounts",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_FacultyID",
                table: "Accounts",
                newName: "IX_Accounts_FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Faculties_FacultyId",
                table: "Accounts",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }
    }
}
