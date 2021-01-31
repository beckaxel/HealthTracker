using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthTracker.Migrations
{
    public partial class RemovedFoodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeverageFood");

            migrationBuilder.DropTable(
                name: "FoodMeal");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.AddColumn<int>(
                name: "Diet",
                table: "Meal",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealType",
                table: "Meal",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diet",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "MealType",
                table: "Meal");

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlcoholContent = table.Column<float>(type: "REAL", nullable: false),
                    ContainsGluten = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContainsLactose = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContainsSugar = table.Column<bool>(type: "INTEGER", nullable: false),
                    Diet = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "BeverageFood",
                columns: table => new
                {
                    BeveragesBeverageId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodsFoodId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageFood", x => new { x.BeveragesBeverageId, x.FoodsFoodId });
                    table.ForeignKey(
                        name: "FK_BeverageFood_Beverage_BeveragesBeverageId",
                        column: x => x.BeveragesBeverageId,
                        principalTable: "Beverage",
                        principalColumn: "BeverageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeverageFood_Food_FoodsFoodId",
                        column: x => x.FoodsFoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodMeal",
                columns: table => new
                {
                    FoodsFoodId = table.Column<int>(type: "INTEGER", nullable: false),
                    MealsMealId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMeal", x => new { x.FoodsFoodId, x.MealsMealId });
                    table.ForeignKey(
                        name: "FK_FoodMeal_Food_FoodsFoodId",
                        column: x => x.FoodsFoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodMeal_Meal_MealsMealId",
                        column: x => x.MealsMealId,
                        principalTable: "Meal",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeverageFood_FoodsFoodId",
                table: "BeverageFood",
                column: "FoodsFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodMeal_MealsMealId",
                table: "FoodMeal",
                column: "MealsMealId");
        }
    }
}
