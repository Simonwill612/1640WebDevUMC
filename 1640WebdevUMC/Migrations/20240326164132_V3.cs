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
                name: "FK_Contributions_AspNetUsers_ApplicationUserId",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_ApplicationUserId",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Contributions");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Contributions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_Email",
                table: "Contributions",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AspNetUsers_Email",
                table: "Contributions",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AspNetUsers_Email",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_Email",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Contributions");

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
                name: "FK_Contributions_AspNetUsers_ApplicationUserId",
                table: "Contributions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
