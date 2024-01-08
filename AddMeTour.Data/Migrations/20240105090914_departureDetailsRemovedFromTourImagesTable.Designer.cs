﻿// <auto-generated />
using System;
using AddMeTour.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AddMeTour.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240105090914_departureDetailsRemovedFromTourImagesTable")]
    partial class departureDetailsRemovedFromTourImagesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AddMeTour.Entity.Entities.Home.Feature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Home.HomeReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserCountry")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("HomeReviews");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Home.Masthead", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BigImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SmallImageUrl1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmallImageUrl2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleWhite")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TitleYellow")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Mastheads");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Home.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("OverallRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Title2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TotalGuestCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Exclusion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ExclusionString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Exclusions");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Inclusion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InclusionString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Inclusions");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Tour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartureDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Duration")
                        .HasColumnType("tinyint");

                    b.Property<byte>("GroupSize")
                        .HasColumnType("tinyint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TourName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("TourId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TourId");

                    b.ToTable("TourCategories");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourCountry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("TourId");

                    b.ToTable("TourCountries");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourExclusion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExclusionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExclusionId");

                    b.HasIndex("TourId");

                    b.ToTable("TourExclusions");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPoster")
                        .HasColumnType("bit");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.ToTable("TourImages");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourInclusion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InclusionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InclusionId");

                    b.HasIndex("TourId");

                    b.ToTable("TourInclusions");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourLanguage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("TourId");

                    b.ToTable("TourLanguages");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourCategory", b =>
                {
                    b.HasOne("AddMeTour.Entity.Entities.Tour.Category", "Category")
                        .WithMany("TourCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AddMeTour.Entity.Entities.Tour.Tour", "Tour")
                        .WithMany("TourCategories")
                        .HasForeignKey("TourId");

                    b.Navigation("Category");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourCountry", b =>
                {
                    b.HasOne("AddMeTour.Entity.Entities.Tour.Country", "Country")
                        .WithMany("TourCountries")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AddMeTour.Entity.Entities.Tour.Tour", "Tour")
                        .WithMany("TourCountries")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourExclusion", b =>
                {
                    b.HasOne("AddMeTour.Entity.Entities.Tour.Exclusion", "Exclusion")
                        .WithMany()
                        .HasForeignKey("ExclusionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AddMeTour.Entity.Entities.Tour.Tour", "Tour")
                        .WithMany("TourExclusions")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exclusion");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourImage", b =>
                {
                    b.HasOne("AddMeTour.Entity.Entities.Tour.Tour", "Tour")
                        .WithMany("TourImages")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourInclusion", b =>
                {
                    b.HasOne("AddMeTour.Entity.Entities.Tour.Inclusion", "Inclusion")
                        .WithMany("TourInclusions")
                        .HasForeignKey("InclusionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AddMeTour.Entity.Entities.Tour.Tour", "Tour")
                        .WithMany("TourInclusions")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inclusion");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.TourLanguage", b =>
                {
                    b.HasOne("AddMeTour.Entity.Entities.Tour.Language", "Language")
                        .WithMany("TourLanguages")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AddMeTour.Entity.Entities.Tour.Tour", "Tour")
                        .WithMany("TourLanguages")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Category", b =>
                {
                    b.Navigation("TourCategories");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Country", b =>
                {
                    b.Navigation("TourCountries");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Inclusion", b =>
                {
                    b.Navigation("TourInclusions");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Language", b =>
                {
                    b.Navigation("TourLanguages");
                });

            modelBuilder.Entity("AddMeTour.Entity.Entities.Tour.Tour", b =>
                {
                    b.Navigation("TourCategories");

                    b.Navigation("TourCountries");

                    b.Navigation("TourExclusions");

                    b.Navigation("TourImages");

                    b.Navigation("TourInclusions");

                    b.Navigation("TourLanguages");
                });
#pragma warning restore 612, 618
        }
    }
}
