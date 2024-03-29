using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContributionItems_Files_FileID",
                table: "ContributionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ContributionItems_Images_ImageID",
                table: "ContributionItems");

            migrationBuilder.AlterColumn<string>(
                name: "ImageID",
                table: "ContributionItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileID",
                table: "ContributionItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContributionItems_Files_FileID",
                table: "ContributionItems",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "FileID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContributionItems_Images_ImageID",
                table: "ContributionItems",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ImageID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContributionItems_Files_FileID",
                table: "ContributionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ContributionItems_Images_ImageID",
                table: "ContributionItems");

            migrationBuilder.AlterColumn<string>(
                name: "ImageID",
                table: "ContributionItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FileID",
                table: "ContributionItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ContributionItems_Files_FileID",
                table: "ContributionItems",
                column: "FileID",
                principalTable: "Files",
                principalColumn: "FileID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContributionItems_Images_ImageID",
                table: "ContributionItems",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ImageID");
        }
    }
}
