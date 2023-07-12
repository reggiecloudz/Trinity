using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trinity.Mvc.Data.Migrations
{
    public partial class TestDonations1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Projects_ProjectId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_ProjectId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Donations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "Donations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_ProjectId",
                table: "Donations",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Projects_ProjectId",
                table: "Donations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
