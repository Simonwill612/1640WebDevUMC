using System;
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
                name: "FK_Files_ContributionItems_ContributionItemID",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_ContributionItems_ContributionItemID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ContributionItemID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Files_ContributionItemID",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ContributionItemID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ContributionItemID",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileData",
                table: "ContributionItems");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "ContributionItems");

            migrationBuilder.RenameColumn(
                name: "ImageData",
                table: "Images",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Images",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "FileContent",
                table: "Files",
                newName: "Data");

            migrationBuilder.AddColumn<string>(
                name: "FileID",
                table: "ContributionItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageID",
                table: "ContributionItems",
                type: "nvarchar(450)",
                nullable: true);

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
                principalColumn: "FileID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContributionItems_Images_ImageID",
                table: "ContributionItems",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ImageID");
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

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Images",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Images",
                newName: "ImageData");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Files",
                newName: "FileContent");

            migrationBuilder.AddColumn<string>(
                name: "ContributionItemID",
                table: "Images",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContributionItemID",
                table: "Files",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FileSize",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Files",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "ContributionItems",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "ContributionItems",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ContributionItemID",
                table: "Images",
                column: "ContributionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ContributionItemID",
                table: "Files",
                column: "ContributionItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_ContributionItems_ContributionItemID",
                table: "Files",
                column: "ContributionItemID",
                principalTable: "ContributionItems",
                principalColumn: "ContributionItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ContributionItems_ContributionItemID",
                table: "Images",
                column: "ContributionItemID",
                principalTable: "ContributionItems",
                principalColumn: "ContributionItemID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
