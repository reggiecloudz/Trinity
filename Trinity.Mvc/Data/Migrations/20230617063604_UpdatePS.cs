using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trinity.Mvc.Data.Migrations
{
    public partial class UpdatePS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSupporters_AspNetUsers_SupportId",
                table: "ProjectSupporters");

            migrationBuilder.DropColumn(
                name: "For",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "SupportId",
                table: "ProjectSupporters",
                newName: "SupporterId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectSupporters_SupportId",
                table: "ProjectSupporters",
                newName: "IX_ProjectSupporters_SupporterId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Events",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSupporters_AspNetUsers_SupporterId",
                table: "ProjectSupporters",
                column: "SupporterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSupporters_AspNetUsers_SupporterId",
                table: "ProjectSupporters");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "SupporterId",
                table: "ProjectSupporters",
                newName: "SupportId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectSupporters_SupporterId",
                table: "ProjectSupporters",
                newName: "IX_ProjectSupporters_SupportId");

            migrationBuilder.AddColumn<string>(
                name: "For",
                table: "Events",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSupporters_AspNetUsers_SupportId",
                table: "ProjectSupporters",
                column: "SupportId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
