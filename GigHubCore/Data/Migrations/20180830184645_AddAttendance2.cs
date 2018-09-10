using Microsoft.EntityFrameworkCore.Migrations;

namespace GigHubCore.Data.Migrations
{
    public partial class AddAttendance2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attendances_GigId",
                table: "Attendances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Attendances_GigId",
                table: "Attendances",
                column: "GigId",
                unique: true);
        }
    }
}
