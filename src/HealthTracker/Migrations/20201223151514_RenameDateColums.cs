using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthTracker.Migrations
{
    public partial class RenameDateColums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Photo",
                newName: "RecordingTime");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Meal",
                newName: "EatingTime");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Drinking",
                newName: "DrinkingTime");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "BodyMeasurement",
                newName: "MeasureTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecordingTime",
                table: "Photo",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "EatingTime",
                table: "Meal",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "DrinkingTime",
                table: "Drinking",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "MeasureTime",
                table: "BodyMeasurement",
                newName: "Date");
        }
    }
}
