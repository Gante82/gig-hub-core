using Microsoft.EntityFrameworkCore.Migrations;

namespace GigHubCore.Data.Migrations
{
    public partial class UpdateFollowUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUp_AspNetUsers_FollowerId",
                table: "FollowUp");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUp_AspNetUsers_FollowerId",
                table: "FollowUp",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowUp_AspNetUsers_FollowerId",
                table: "FollowUp");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUp_AspNetUsers_FollowerId",
                table: "FollowUp",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
