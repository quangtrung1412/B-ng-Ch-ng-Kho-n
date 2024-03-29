﻿// <auto-generated />
using Exam01.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Exam01.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Exam01.Application.Models.Stock", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<double>("BanG1")
                        .HasColumnType("REAL")
                        .HasColumnName("BanG1");

                    b.Property<double>("BanG2")
                        .HasColumnType("REAL")
                        .HasColumnName("BanG2");

                    b.Property<double>("BanG3")
                        .HasColumnType("REAL")
                        .HasColumnName("BanG3");

                    b.Property<int>("BanKL1")
                        .HasColumnType("INTEGER")
                        .HasColumnName("BanKL1");

                    b.Property<int>("BanKL2")
                        .HasColumnType("INTEGER")
                        .HasColumnName("BanKL2");

                    b.Property<int>("BanKL3")
                        .HasColumnType("INTEGER")
                        .HasColumnName("BanKL3");

                    b.Property<double>("CaoNhat")
                        .HasColumnType("REAL")
                        .HasColumnName("CaoNhat");

                    b.Property<double>("Gia")
                        .HasColumnType("REAL")
                        .HasColumnName("Gia");

                    b.Property<int>("KL")
                        .HasColumnType("INTEGER")
                        .HasColumnName("KL");

                    b.Property<double>("MoCua")
                        .HasColumnType("REAL")
                        .HasColumnName("MoCua");

                    b.Property<double>("MuaG1")
                        .HasColumnType("REAL")
                        .HasColumnName("MuaG1");

                    b.Property<double>("MuaG2")
                        .HasColumnType("REAL")
                        .HasColumnName("MuaG2");

                    b.Property<double>("MuaG3")
                        .HasColumnType("REAL")
                        .HasColumnName("MuaG3");

                    b.Property<int>("MuaKL1")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MuaKL1");

                    b.Property<int>("MuaKL2")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MuaKL2");

                    b.Property<int>("MuaKL3")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MuaKL3");

                    b.Property<int>("NNBan")
                        .HasColumnType("INTEGER")
                        .HasColumnName("NNBan");

                    b.Property<int>("NNMua")
                        .HasColumnType("INTEGER")
                        .HasColumnName("NNMua");

                    b.Property<double>("Percent")
                        .HasColumnType("REAL")
                        .HasColumnName("Percent");

                    b.Property<int>("RoomConLai")
                        .HasColumnType("INTEGER")
                        .HasColumnName("RoomConLai");

                    b.Property<double>("San")
                        .HasColumnType("REAL")
                        .HasColumnName("San");

                    b.Property<double>("TC")
                        .HasColumnType("REAL")
                        .HasColumnName("TC");

                    b.Property<double>("ThapNhat")
                        .HasColumnType("REAL")
                        .HasColumnName("ThapNhat");

                    b.Property<int>("TongKL")
                        .HasColumnType("INTEGER")
                        .HasColumnName("TongKL");

                    b.Property<double>("Tran")
                        .HasColumnType("REAL")
                        .HasColumnName("Tran");

                    b.HasKey("Id")
                        .HasName("Stock_Id_Pk");

                    b.ToTable("Stock", (string)null);
                });

            modelBuilder.Entity("Exam01.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Avatar")
                        .HasColumnType("TEXT")
                        .HasColumnName("Avatar");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasColumnName("Password");

                    b.Property<string>("TemPass")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasColumnName("TemPass");

                    b.HasKey("Id")
                        .HasName("User_Id_Pk");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("User_Email_Uq");

                    b.ToTable("User", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
