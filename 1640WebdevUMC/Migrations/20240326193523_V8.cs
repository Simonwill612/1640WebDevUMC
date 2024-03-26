using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1640WebDevUMC.Migrations
{
    /// <inheritdoc />
    public partial class V8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "CommentDate",
                table: "Contributions");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Contributions",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Contributions",
                newName: "File");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Contributions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "File",
                table: "Contributions",
                newName: "Content");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CommentDate",
                table: "Contributions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
