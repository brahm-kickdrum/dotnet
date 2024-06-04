﻿// <auto-generated />
using System;
using Assessment_1.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Assessment_1.Migrations
{
    [DbContext(typeof(RideDbContext))]
    [Migration("20240604104253_ForiegnKey")]
    partial class ForiegnKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Assessment_1.Entities.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LicenceNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Drivers", (string)null);
                });

            modelBuilder.Entity("Assessment_1.Entities.Ride", b =>
                {
                    b.Property<Guid>("RideId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uuid");

                    b.Property<string>("EndLocation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Fare")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("RideEndDT")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("RideRequestDT")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("RideStartDT")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RiderId")
                        .HasColumnType("uuid");

                    b.Property<string>("StartLocation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VehicleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RideId");

                    b.HasIndex("DriverId");

                    b.HasIndex("RiderId");

                    b.ToTable("Rides", (string)null);
                });

            modelBuilder.Entity("Assessment_1.Entities.Rider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Riders", (string)null);
                });

            modelBuilder.Entity("Assessment_1.Entities.Ride", b =>
                {
                    b.HasOne("Assessment_1.Entities.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assessment_1.Entities.Rider", "Rider")
                        .WithMany()
                        .HasForeignKey("RiderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Rider");
                });
#pragma warning restore 612, 618
        }
    }
}
