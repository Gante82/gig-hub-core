using Microsoft.EntityFrameworkCore.Migrations;

namespace GigHubCore.Data.Migrations
{
    public partial class AddNameColumnToGigsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Band",
                table: "AspNetUsers",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "Band");
        }
    }
}
