using System;
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
                name: "FK_Comments_ContributionItems_ContributionItemID",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "ContributionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ContributionItemID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "FileID",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileData",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ContributionItemID",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ContributionID",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ContributionID",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "FileID",
                table: "Files",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "Files",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ContributionItemID",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "FileID");

            migrationBuilder.CreateTable(
                name: "ContributionItems",
                columns: table => new
                {
                    ContributionItemID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContributionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributionItems", x => x.ContributionItemID);
                    table.ForeignKey(
                        name: "FK_ContributionItems_Contributions_ContributionID",
                        column: x => x.ContributionID,
                        principalTable: "Contributions",
                        principalColumn: "ContributionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContributionItems_Files_FileID",
                        column: x => x.FileID,
                        principalTable: "Files",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContributionItems_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ContributionItemID",
                table: "Comments",
                column: "ContributionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionItems_ContributionID",
                table: "ContributionItems",
                column: "ContributionID");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionItems_FileID",
                table: "ContributionItems",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionItems_ImageID",
                table: "ContributionItems",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ContributionItems_ContributionItemID",
                table: "Comments",
                column: "ContributionItemID",
                principalTable: "ContributionItems",
                principalColumn: "ContributionItemID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
