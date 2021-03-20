﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScratchPad.Data;

namespace ScratchPad.Migrations
{
    [DbContext(typeof(ScratchPadContext))]
    partial class ScratchPadContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("CategoryEntry", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntriesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoriesId", "EntriesId");

                    b.HasIndex("EntriesId");

                    b.ToTable("CategoryEntry");
                });

            modelBuilder.Entity("ScratchPad.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ScratchPad.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EntryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ScratchPad.Models.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("CategoryEntry", b =>
                {
                    b.HasOne("ScratchPad.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScratchPad.Models.Entry", null)
                        .WithMany()
                        .HasForeignKey("EntriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScratchPad.Models.Comment", b =>
                {
                    b.HasOne("ScratchPad.Models.Entry", "Entry")
                        .WithMany("Comments")
                        .HasForeignKey("EntryId");

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("ScratchPad.Models.Entry", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
