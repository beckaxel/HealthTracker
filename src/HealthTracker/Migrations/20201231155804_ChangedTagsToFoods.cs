using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthTracker.Migrations
{
    public partial class ChangedTagsToFoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeverageTag");

            migrationBuilder.DropTable(
                name: "BodyMeasurementTag");

            migrationBuilder.DropTable(
                name: "MealTag");

            migrationBuilder.DropTable(
                name: "SleepLogTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Diet = table.Column<int>(type: "INTEGER", nullable: false),
                    AlcoholContent = table.Column<float>(type: "REAL", nullable: false),
                    ContainsSugar = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContainsLactose = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContainsGluten = table.Column<bool>(type: "INTEGER", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeverageFood");

            migrationBuilder.DropTable(
                name: "FoodMeal");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "BeverageTag",
                columns: table => new
                {
                    BeveragesBeverageId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsTagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageTag", x => new { x.BeveragesBeverageId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_BeverageTag_Beverage_BeveragesBeverageId",
                        column: x => x.BeveragesBeverageId,
                        principalTable: "Beverage",
                        principalColumn: "BeverageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeverageTag_Tag_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BodyMeasurementTag",
                columns: table => new
                {
                    BodyMeasurementsBodyMeasurementId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsTagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurementTag", x => new { x.BodyMeasurementsBodyMeasurementId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_BodyMeasurementTag_BodyMeasurement_BodyMeasurementsBodyMeasurementId",
                        column: x => x.BodyMeasurementsBodyMeasurementId,
                        principalTable: "BodyMeasurement",
                        principalColumn: "BodyMeasurementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyMeasurementTag_Tag_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealTag",
                columns: table => new
                {
                    MealsMealId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsTagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTag", x => new { x.MealsMealId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_MealTag_Meal_MealsMealId",
                        column: x => x.MealsMealId,
                        principalTable: "Meal",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealTag_Tag_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SleepLogTag",
                columns: table => new
                {
                    SleepLogsSleepLogId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsTagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepLogTag", x => new { x.SleepLogsSleepLogId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_SleepLogTag_SleepLog_SleepLogsSleepLogId",
                        column: x => x.SleepLogsSleepLogId,
                        principalTable: "SleepLog",
                        principalColumn: "SleepLogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SleepLogTag_Tag_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeverageTag_TagsTagId",
                table: "BeverageTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurementTag_TagsTagId",
                table: "BodyMeasurementTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTag_TagsTagId",
                table: "MealTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_SleepLogTag_TagsTagId",
                table: "SleepLogTag",
                column: "TagsTagId");
        }
    }
}
