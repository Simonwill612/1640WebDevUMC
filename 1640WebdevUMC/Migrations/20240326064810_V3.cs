using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYears_Faculties_FacultyId",
                table: "AcademicYears");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "AspNetUsers",
                newName: "FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_FacultyId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_FacultyID");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "AcademicYears",
                newName: "FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYears_FacultyId",
                table: "AcademicYears",
                newName: "IX_AcademicYears_FacultyID");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FacultyID",
                table: "AcademicYears",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.RenameColumn(
                name: "FacultyID",
                table: "AspNetUsers",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_FacultyID",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_FacultyId");

            migrationBuilder.RenameColumn(
                name: "FacultyID",
                table: "AcademicYears",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicYears_FacultyID",
                table: "AcademicYears",
                newName: "IX_AcademicYears_FacultyId");

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
                name: "FacultyId",
                table: "AcademicYears",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYears_Faculties_FacultyId",
                table: "AcademicYears",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyId",
                table: "AspNetUsers",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyID");
        }
    }
}
