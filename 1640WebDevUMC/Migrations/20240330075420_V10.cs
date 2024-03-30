using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Migrations
{
    /// <inheritdoc />
    public partial class V10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ContributionItems");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ContributionItems");

            migrationBuilder.AddColumn<string>(
                name: "FileID",
                table: "ContributionItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageID",
                table: "ContributionItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionItems_FileID",
                table: "ContributionItems",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionItems_ImageID",
                table: "ContributionItems",
                column: "ImageID");

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

            migrationBuilder.DropIndex(
                name: "IX_ContributionItems_FileID",
                table: "ContributionItems");

            migrationBuilder.DropIndex(
                name: "IX_ContributionItems_ImageID",
                table: "ContributionItems");

            migrationBuilder.DropColumn(
                name: "FileID",
                table: "ContributionItems");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "ContributionItems");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ContributionItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ContributionItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
