﻿// <auto-generated />
using System;
using HealthTracker.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HealthTracker.Migrations
{
    [DbContext(typeof(HealthTrackerDbContext))]
    [Migration("20201231155804_ChangedTagsToFoods")]
    partial class ChangedTagsToFoods
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("BeverageFood", b =>
                {
                    b.Property<int>("BeveragesBeverageId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FoodsFoodId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BeveragesBeverageId", "FoodsFoodId");

                    b.HasIndex("FoodsFoodId");

                    b.ToTable("BeverageFood");
                });

            modelBuilder.Entity("BeveragePhoto", b =>
                {
                    b.Property<int>("BeveragesBeverageId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PhotosPhotoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BeveragesBeverageId", "PhotosPhotoId");

                    b.HasIndex("PhotosPhotoId");

                    b.ToTable("BeveragePhoto");
                });

            modelBuilder.Entity("FoodMeal", b =>
                {
                    b.Property<int>("FoodsFoodId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MealsMealId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FoodsFoodId", "MealsMealId");

                    b.HasIndex("MealsMealId");

                    b.ToTable("FoodMeal");
                });

            modelBuilder.Entity("HealthTracker.Models.Beverage", b =>
                {
                    b.Property<int>("BeverageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("DrinkingTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("BeverageId");

                    b.ToTable("Beverage");
                });

            modelBuilder.Entity("HealthTracker.Models.BodyMeasurement", b =>
                {
                    b.Property<int>("BodyMeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("MeasureTime")
                        .HasColumnType("TEXT");

                    b.Property<float?>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("BodyMeasurementId");

                    b.ToTable("BodyMeasurement");
                });

            modelBuilder.Entity("HealthTracker.Models.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("AlcoholContent")
                        .HasColumnType("REAL");

                    b.Property<bool>("ContainsGluten")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ContainsLactose")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ContainsSugar")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Diet")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("FoodId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("HealthTracker.Models.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EatingTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("MealId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("HealthTracker.Models.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RecordingTime")
                        .HasColumnType("TEXT");

                    b.HasKey("PhotoId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("HealthTracker.Models.Setting", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("SettingId");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("HealthTracker.Models.SleepLog", b =>
                {
                    b.Property<int>("SleepLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BedTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FallAsleepTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("GetUpTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LightOffTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("WakeUpTime")
                        .HasColumnType("TEXT");

                    b.HasKey("SleepLogId");

                    b.ToTable("SleepLog");
                });

            modelBuilder.Entity("HealthTracker.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Height")
                        .HasColumnType("REAL");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HealthTracker.Models.Waking", b =>
                {
                    b.Property<int>("WakingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<int>("SleepLogId")
                        .HasColumnType("INTEGER");

                    b.HasKey("WakingId");

                    b.HasIndex("SleepLogId");

                    b.ToTable("Waking");
                });

            modelBuilder.Entity("MealPhoto", b =>
                {
                    b.Property<int>("MealsMealId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PhotosPhotoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MealsMealId", "PhotosPhotoId");

                    b.HasIndex("PhotosPhotoId");

                    b.ToTable("MealPhoto");
                });

            modelBuilder.Entity("BeverageFood", b =>
                {
                    b.HasOne("HealthTracker.Models.Beverage", null)
                        .WithMany()
                        .HasForeignKey("BeveragesBeverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthTracker.Models.Food", null)
                        .WithMany()
                        .HasForeignKey("FoodsFoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeveragePhoto", b =>
                {
                    b.HasOne("HealthTracker.Models.Beverage", null)
                        .WithMany()
                        .HasForeignKey("BeveragesBeverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthTracker.Models.Photo", null)
                        .WithMany()
                        .HasForeignKey("PhotosPhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodMeal", b =>
                {
                    b.HasOne("HealthTracker.Models.Food", null)
                        .WithMany()
                        .HasForeignKey("FoodsFoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthTracker.Models.Meal", null)
                        .WithMany()
                        .HasForeignKey("MealsMealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HealthTracker.Models.Waking", b =>
                {
                    b.HasOne("HealthTracker.Models.SleepLog", "SleepLog")
                        .WithMany("Wakings")
                        .HasForeignKey("SleepLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SleepLog");
                });

            modelBuilder.Entity("MealPhoto", b =>
                {
                    b.HasOne("HealthTracker.Models.Meal", null)
                        .WithMany()
                        .HasForeignKey("MealsMealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthTracker.Models.Photo", null)
                        .WithMany()
                        .HasForeignKey("PhotosPhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HealthTracker.Models.SleepLog", b =>
                {
                    b.Navigation("Wakings");
                });
#pragma warning restore 612, 618
        }
    }
}
