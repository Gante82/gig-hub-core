using Microsoft.EntityFrameworkCore.Migrations;

namespace GigHubCore.Data.Migrations
{
    public partial class AddFollowUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FollowUp",
                columns: table => new
                {
                    ArtistId = table.Column<string>(maxLength: 450, nullable: false),
                    FollowerId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUp", x => new { x.ArtistId, x.FollowerId });
                    table.ForeignKey(
                        name: "FK_FollowUp_AspNetUsers_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowUp_AspNetUsers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowUp_FollowerId",
                table: "FollowUp",
                column: "FollowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowUp");
        }
    }
}
