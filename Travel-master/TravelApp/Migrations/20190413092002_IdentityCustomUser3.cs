using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelApp.Migrations
{
    public partial class IdentityCustomUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wctcid",
                table: "AspNetUsers",
                newName: "WctcID");

            migrationBuilder.AddColumn<string>(
                name: "ProgramID",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "WctcID",
                table: "AspNetUsers",
                newName: "Wctcid");
        }
    }
}
