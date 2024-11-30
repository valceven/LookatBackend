﻿// <auto-generated />
using System;
using LookatBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LookatBackend.Migrations
{
    [DbContext(typeof(LookatDbContext))]
    [Migration("20241130115919_OtpVerify")]
    partial class OtpVerify
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LookatBackend.Models.Barangay", b =>
                {
                    b.Property<string>("BarangayId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BarangayLoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BarangayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityMunicipality")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Purok")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BarangayId");

                    b.ToTable("Barangays");
                });

            modelBuilder.Entity("LookatBackend.Models.DocumentType", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentId"));

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("DocumentId");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("LookatBackend.Models.OtpRecords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Otp")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OtpRecords");
                });

            modelBuilder.Entity("LookatBackend.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RequestType")
                        .HasColumnType("int");

                    b.HasKey("RequestId");

                    b.HasIndex("DocumentId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("LookatBackend.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("BarangayId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BarangayLoc")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CityMunicipality")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhysicalIdNumber")
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Purok")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.HasIndex("BarangayId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LookatBackend.Models.Request", b =>
                {
                    b.HasOne("LookatBackend.Models.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");
                });

            modelBuilder.Entity("LookatBackend.Models.User", b =>
                {
                    b.HasOne("LookatBackend.Models.Barangay", "Barangay")
                        .WithMany()
                        .HasForeignKey("BarangayId");

                    b.Navigation("Barangay");
                });
#pragma warning restore 612, 618
        }
    }
}
