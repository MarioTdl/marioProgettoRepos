﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using marioProgetto.Persistence;

namespace marioProgetto.Migrations
{
    [DbContext(typeof(MarioProgettoDbContext))]
    [Migration("20190830133456_AddPhoto")]
    partial class AddPhoto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("marioProgetto.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("marioProgetto.Models.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("marioProgetto.Models.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MakeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("marioProgetto.Models.Veichle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactEmail")
                        .HasMaxLength(255);

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("IsRegistered");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("ModelId");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Veichles");
                });

            modelBuilder.Entity("marioProgetto.Models.VeichleFeature", b =>
                {
                    b.Property<int>("VeichleId");

                    b.Property<int>("FeatureId");

                    b.HasKey("VeichleId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("VeichleFeatures");
                });

            modelBuilder.Entity("marioProgettoRepos.Core.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int?>("VeichleId");

                    b.HasKey("Id");

                    b.HasIndex("VeichleId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("marioProgetto.Models.Model", b =>
                {
                    b.HasOne("marioProgetto.Models.Make", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("marioProgetto.Models.Veichle", b =>
                {
                    b.HasOne("marioProgetto.Models.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("marioProgetto.Models.VeichleFeature", b =>
                {
                    b.HasOne("marioProgetto.Models.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("marioProgetto.Models.Veichle", "Veichle")
                        .WithMany("Features")
                        .HasForeignKey("VeichleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("marioProgettoRepos.Core.Models.Photo", b =>
                {
                    b.HasOne("marioProgetto.Models.Veichle")
                        .WithMany("Photos")
                        .HasForeignKey("VeichleId");
                });
#pragma warning restore 612, 618
        }
    }
}