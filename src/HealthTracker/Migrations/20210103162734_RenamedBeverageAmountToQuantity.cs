using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthTracker.Migrations
{
    public partial class RenamedBeverageAmountToQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Beverage",
                newName: "Quantity");

            migrationBuilder.AddColumn<float>(
                name: "DailyDrinkingQuantity",
                table: "User",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyDrinkingQuantity",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Beverage",
                newName: "Amount");
        }
    }
}
