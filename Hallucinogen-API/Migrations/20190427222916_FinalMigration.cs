using Microsoft.EntityFrameworkCore.Migrations;

namespace Hallucinogen_API.Migrations
{
    public partial class FinalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_Latitude",
                table: "Posts",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Longitude",
                table: "Posts",
                column: "Longitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_Latitude",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Longitude",
                table: "Posts");
        }
    }
}
