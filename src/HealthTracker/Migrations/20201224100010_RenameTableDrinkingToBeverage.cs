using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthTracker.Migrations
{
    public partial class RenameTableDrinkingToBeverage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeveragePhoto_Drinking_BeveragesBeverageId",
                table: "BeveragePhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_BeverageTag_Drinking_BeveragesBeverageId",
                table: "BeverageTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drinking",
                table: "Drinking");

            migrationBuilder.RenameTable(
                name: "Drinking",
                newName: "Beverage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beverage",
                table: "Beverage",
                column: "BeverageId");

            migrationBuilder.AddForeignKey(
                name: "FK_BeveragePhoto_Beverage_BeveragesBeverageId",
                table: "BeveragePhoto",
                column: "BeveragesBeverageId",
                principalTable: "Beverage",
                principalColumn: "BeverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BeverageTag_Beverage_BeveragesBeverageId",
                table: "BeverageTag",
                column: "BeveragesBeverageId",
                principalTable: "Beverage",
                principalColumn: "BeverageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeveragePhoto_Beverage_BeveragesBeverageId",
                table: "BeveragePhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_BeverageTag_Beverage_BeveragesBeverageId",
                table: "BeverageTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beverage",
                table: "Beverage");

            migrationBuilder.RenameTable(
                name: "Beverage",
                newName: "Drinking");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drinking",
                table: "Drinking",
                column: "BeverageId");

            migrationBuilder.AddForeignKey(
                name: "FK_BeveragePhoto_Drinking_BeveragesBeverageId",
                table: "BeveragePhoto",
                column: "BeveragesBeverageId",
                principalTable: "Drinking",
                principalColumn: "BeverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BeverageTag_Drinking_BeveragesBeverageId",
                table: "BeverageTag",
                column: "BeveragesBeverageId",
                principalTable: "Drinking",
                principalColumn: "BeverageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
