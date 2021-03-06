﻿// <auto-generated />
using System;
using GrecosQuestionnaire.Data;
using GrecosQuestionnaire.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GrecosQuestionnaire.Migrations
{
    [DbContext(typeof(HotelDBContext))]
    [Migration("20200427105248_hotels6")]
    partial class hotels6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GrecosQuestionnaire.Models.HotelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Destination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationSeasonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("GrecosQuestionnaire.Models.MainRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HotelModelId")
                        .HasColumnType("int");

                    b.Property<string>("MainRoomCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainRoomName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelModelId");

                    b.ToTable("MainRoom");
                });

            modelBuilder.Entity("GrecosQuestionnaire.Models.SharedUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MainRoomId")
                        .HasColumnType("int");

                    b.Property<string>("SharedRoomCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MainRoomId");

                    b.ToTable("SharedUnit");
                });

            modelBuilder.Entity("GrecosQuestionnaire.Models.MainRoom", b =>
                {
                    b.HasOne("GrecosQuestionnaire.Models.HotelModel", null)
                        .WithMany("MainRooms")
                        .HasForeignKey("HotelModelId");
                });

            modelBuilder.Entity("GrecosQuestionnaire.Models.SharedUnit", b =>
                {
                    b.HasOne("GrecosQuestionnaire.Models.MainRoom", null)
                        .WithMany("SharedUnits")
                        .HasForeignKey("MainRoomId");
                });
#pragma warning restore 612, 618
        }
    }
}
