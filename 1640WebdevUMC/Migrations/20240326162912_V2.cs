using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AcademicYears_AcademicYearID",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AspNetUsers_Id",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_Id",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Contributions");

            migrationBuilder.AlterColumn<string>(
                name: "AcademicYearID",
                table: "Contributions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Contributions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_ApplicationUserId",
                table: "Contributions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AcademicYears_AcademicYearID",
                table: "Contributions",
                column: "AcademicYearID",
                principalTable: "AcademicYears",
                principalColumn: "AcademicYearID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AspNetUsers_ApplicationUserId",
                table: "Contributions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AcademicYears_AcademicYearID",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AspNetUsers_ApplicationUserId",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_ApplicationUserId",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Contributions");

            migrationBuilder.AlterColumn<string>(
                name: "AcademicYearID",
                table: "Contributions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Contributions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_Id",
                table: "Contributions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AcademicYears_AcademicYearID",
                table: "Contributions",
                column: "AcademicYearID",
                principalTable: "AcademicYears",
                principalColumn: "AcademicYearID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AspNetUsers_Id",
                table: "Contributions",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
