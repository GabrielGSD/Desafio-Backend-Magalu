﻿// <auto-generated />
using System;
using DesafioMagalu.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DesafioMagalu.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DesafioMagalu.Models.ChannelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Channels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "email"
                        },
                        new
                        {
                            Id = 2,
                            Description = "sms"
                        },
                        new
                        {
                            Id = 3,
                            Description = "push"
                        },
                        new
                        {
                            Id = 4,
                            Description = "whatsapp"
                        });
                });

            modelBuilder.Entity("DesafioMagalu.Models.NotificationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChannelId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("StatusId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("DesafioMagalu.Models.StatusModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "pending"
                        },
                        new
                        {
                            Id = 2,
                            Description = "success"
                        },
                        new
                        {
                            Id = 3,
                            Description = "error"
                        },
                        new
                        {
                            Id = 4,
                            Description = "canceled"
                        });
                });

            modelBuilder.Entity("DesafioMagalu.Models.NotificationModel", b =>
                {
                    b.HasOne("DesafioMagalu.Models.ChannelModel", "Channel")
                        .WithMany("Notifications")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DesafioMagalu.Models.StatusModel", "Status")
                        .WithMany("Notifications")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DesafioMagalu.Models.ChannelModel", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("DesafioMagalu.Models.StatusModel", b =>
                {
                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
