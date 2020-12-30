using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthTracker.Migrations
{
    public partial class AddedFileNameColumnForPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Photo",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Photo");
        }
    }
}
