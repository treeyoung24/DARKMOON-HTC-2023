﻿// <auto-generated />
using Learning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(GeneralContext))]
    partial class GeneralContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Backend.Models.DTO.Driver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PoolId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("DriverId");

                    b.ToTable("Driver");
                });

            modelBuilder.Entity("Backend.Models.Passenger", b =>
                {
                    b.Property<int>("PassengerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Fee")
                        .HasColumnType("REAL");

                    b.Property<string>("PickupTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("PoolId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PassengerId");

                    b.ToTable("Passenger");
                });

            modelBuilder.Entity("Backend.Models.Pool", b =>
                {
                    b.Property<int>("PoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArrivalTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Destination")
                        .HasColumnType("TEXT");

                    b.Property<int>("HostId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PoolSize")
                        .HasColumnType("INTEGER");

                    b.HasKey("PoolId");

                    b.ToTable("Pool");
                });

            modelBuilder.Entity("Backend.Models.RouteOrder", b =>
                {
                    b.Property<int>("Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RouteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Order");

                    b.ToTable("RouteOrder");
                });

            modelBuilder.Entity("Backend.Models.Routes", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Distance")
                        .HasColumnType("REAL");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Polylines")
                        .HasColumnType("TEXT");

                    b.HasKey("RouteId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Backend.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
