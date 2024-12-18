﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UI.Cont;

#nullable disable

namespace UI.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WordsGame.CatWord", b =>
                {
                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CatWordId")
                        .HasColumnType("int");

                    b.HasKey("WordId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CatWords");
                });

            modelBuilder.Entity("WordsGame.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WordsGame.Difficulty", b =>
                {
                    b.Property<int>("DifficultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DifficultyId"));

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DifficultyId");

                    b.ToTable("Difficulties");
                });

            modelBuilder.Entity("WordsGame.Score", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("date_achieved")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("wordID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("wordID");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("WordsGame.Word", b =>
                {
                    b.Property<int>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WordId"));

                    b.Property<int>("DifficultyId")
                        .HasColumnType("int");

                    b.Property<string>("WordText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WordId");

                    b.HasIndex("DifficultyId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("WordsGame.CatWord", b =>
                {
                    b.HasOne("WordsGame.Category", "Category")
                        .WithMany("CatWords")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WordsGame.Word", "Word")
                        .WithMany("CatWords")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("WordsGame.Score", b =>
                {
                    b.HasOne("WordsGame.Word", "word")
                        .WithMany("Scores")
                        .HasForeignKey("wordID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("word");
                });

            modelBuilder.Entity("WordsGame.Word", b =>
                {
                    b.HasOne("WordsGame.Difficulty", "Difficulty")
                        .WithMany("Words")
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");
                });

            modelBuilder.Entity("WordsGame.Category", b =>
                {
                    b.Navigation("CatWords");
                });

            modelBuilder.Entity("WordsGame.Difficulty", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("WordsGame.Word", b =>
                {
                    b.Navigation("CatWords");

                    b.Navigation("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}