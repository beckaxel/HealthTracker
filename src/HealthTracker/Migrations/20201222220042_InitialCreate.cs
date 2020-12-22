using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthTracker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyMeasurement",
                columns: table => new
                {
                    BodyMeasurementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Weight = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurement", x => x.BodyMeasurementId);
                });

            migrationBuilder.CreateTable(
                name: "Drinking",
                columns: table => new
                {
                    BeverageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinking", x => x.BeverageId);
                });

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.MealId);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.PhotoId);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingId);
                });

            migrationBuilder.CreateTable(
                name: "SleepLog",
                columns: table => new
                {
                    SleepLogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LightOffTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FallAsleepTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WakeUpTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GetUpTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepLog", x => x.SleepLogId);
                });

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
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Height = table.Column<float>(type: "REAL", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BeveragePhoto",
                columns: table => new
                {
                    BeveragesBeverageId = table.Column<int>(type: "INTEGER", nullable: false),
                    PhotosPhotoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeveragePhoto", x => new { x.BeveragesBeverageId, x.PhotosPhotoId });
                    table.ForeignKey(
                        name: "FK_BeveragePhoto_Drinking_BeveragesBeverageId",
                        column: x => x.BeveragesBeverageId,
                        principalTable: "Drinking",
                        principalColumn: "BeverageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeveragePhoto_Photo_PhotosPhotoId",
                        column: x => x.PhotosPhotoId,
                        principalTable: "Photo",
                        principalColumn: "PhotoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPhoto",
                columns: table => new
                {
                    MealsMealId = table.Column<int>(type: "INTEGER", nullable: false),
                    PhotosPhotoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPhoto", x => new { x.MealsMealId, x.PhotosPhotoId });
                    table.ForeignKey(
                        name: "FK_MealPhoto_Meal_MealsMealId",
                        column: x => x.MealsMealId,
                        principalTable: "Meal",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPhoto_Photo_PhotosPhotoId",
                        column: x => x.PhotosPhotoId,
                        principalTable: "Photo",
                        principalColumn: "PhotoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Waking",
                columns: table => new
                {
                    WakingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SleepLogId = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waking", x => x.WakingId);
                    table.ForeignKey(
                        name: "FK_Waking_SleepLog_SleepLogId",
                        column: x => x.SleepLogId,
                        principalTable: "SleepLog",
                        principalColumn: "SleepLogId",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_BeverageTag_Drinking_BeveragesBeverageId",
                        column: x => x.BeveragesBeverageId,
                        principalTable: "Drinking",
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
                name: "IX_BeveragePhoto_PhotosPhotoId",
                table: "BeveragePhoto",
                column: "PhotosPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_BeverageTag_TagsTagId",
                table: "BeverageTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurementTag_TagsTagId",
                table: "BodyMeasurementTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPhoto_PhotosPhotoId",
                table: "MealPhoto",
                column: "PhotosPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTag_TagsTagId",
                table: "MealTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_SleepLogTag_TagsTagId",
                table: "SleepLogTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Waking_SleepLogId",
                table: "Waking",
                column: "SleepLogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeveragePhoto");

            migrationBuilder.DropTable(
                name: "BeverageTag");

            migrationBuilder.DropTable(
                name: "BodyMeasurementTag");

            migrationBuilder.DropTable(
                name: "MealPhoto");

            migrationBuilder.DropTable(
                name: "MealTag");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "SleepLogTag");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Waking");

            migrationBuilder.DropTable(
                name: "Drinking");

            migrationBuilder.DropTable(
                name: "BodyMeasurement");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Meal");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "SleepLog");
        }
    }
}
