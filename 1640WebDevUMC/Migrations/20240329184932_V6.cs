using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Migrations
{
    /// <inheritdoc />
    public partial class V6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContributionItems_Files_FileName",
                table: "ContributionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ContributionItems_Images_ImageName",
                table: "ContributionItems");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "ContributionItems",
                newName: "ImageID");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ContributionItems",
                newName: "FileID");

            migrationBuilder.RenameIndex(
                name: "IX_ContributionItems_ImageName",
                table: "ContributionItems",
                newName: "IX_ContributionItems_ImageID");

            migrationBuilder.RenameIndex(
                name: "IX_ContributionItems_FileName",
                table: "ContributionItems",
                newName: "IX_ContributionItems_FileID");

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

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "ContributionItems",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "FileID",
                table: "ContributionItems",
                newName: "FileName");

            migrationBuilder.RenameIndex(
                name: "IX_ContributionItems_ImageID",
                table: "ContributionItems",
                newName: "IX_ContributionItems_ImageName");

            migrationBuilder.RenameIndex(
                name: "IX_ContributionItems_FileID",
                table: "ContributionItems",
                newName: "IX_ContributionItems_FileName");

            migrationBuilder.AddForeignKey(
                name: "FK_ContributionItems_Files_FileName",
                table: "ContributionItems",
                column: "FileName",
                principalTable: "Files",
                principalColumn: "FileID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContributionItems_Images_ImageName",
                table: "ContributionItems",
                column: "ImageName",
                principalTable: "Images",
                principalColumn: "ImageID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
