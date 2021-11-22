﻿// <auto-generated />
using System;
using Discord_Bot.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Discord_Bot.Migrations
{
    [DbContext(typeof(DiscordBotEntities))]
    [Migration("20211121235722_ActorKeyMigration")]
    partial class ActorKeyMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("CharacterFight", b =>
                {
                    b.Property<long>("AlliesId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("FightsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlliesId", "FightsId");

                    b.HasIndex("FightsId");

                    b.ToTable("CharacterFight");
                });

            modelBuilder.Entity("Discord_Bot.Database.EightBallAnswer", b =>
                {
                    b.Property<long>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnswerColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("AnswerText")
                        .HasColumnType("TEXT");

                    b.HasKey("AnswerId");

                    b.ToTable("EightBallAnswer");
                });

            modelBuilder.Entity("Discord_Bot.Database.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ActorId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("FightId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("TargetId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("FightId");

                    b.HasIndex("TargetId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Discord_Bot.Database.FiendType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaseEnergy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaseHealth")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FiendType");
                });

            modelBuilder.Entity("Discord_Bot.Database.Fight", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsGlobal")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Fight");
                });

            modelBuilder.Entity("Discord_Bot.Utils.Actor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Energy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Health")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxEnergy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxHealth")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Actor");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Actor");
                });

            modelBuilder.Entity("FiendFight", b =>
                {
                    b.Property<long>("FiendsId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("FightsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FiendsId", "FightsId");

                    b.HasIndex("FightsId");

                    b.ToTable("FiendFight");
                });

            modelBuilder.Entity("Discord_Bot.Database.Character", b =>
                {
                    b.HasBaseType("Discord_Bot.Utils.Actor");

                    b.Property<string>("DiscordDiscriminator")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("DiscordId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Experience")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Character");
                });

            modelBuilder.Entity("Discord_Bot.Database.Fiend", b =>
                {
                    b.HasBaseType("Discord_Bot.Utils.Actor");

                    b.Property<long?>("FiendTypeId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("FiendTypeId");

                    b.HasDiscriminator().HasValue("Fiend");
                });

            modelBuilder.Entity("CharacterFight", b =>
                {
                    b.HasOne("Discord_Bot.Database.Character", null)
                        .WithMany()
                        .HasForeignKey("AlliesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Discord_Bot.Database.Fight", null)
                        .WithMany()
                        .HasForeignKey("FightsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Discord_Bot.Database.Event", b =>
                {
                    b.HasOne("Discord_Bot.Utils.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId");

                    b.HasOne("Discord_Bot.Database.Fight", "Fight")
                        .WithMany("Events")
                        .HasForeignKey("FightId");

                    b.HasOne("Discord_Bot.Utils.Actor", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId");

                    b.Navigation("Actor");

                    b.Navigation("Fight");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("FiendFight", b =>
                {
                    b.HasOne("Discord_Bot.Database.Fiend", null)
                        .WithMany()
                        .HasForeignKey("FiendsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Discord_Bot.Database.Fight", null)
                        .WithMany()
                        .HasForeignKey("FightsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Discord_Bot.Database.Fiend", b =>
                {
                    b.HasOne("Discord_Bot.Database.FiendType", "FiendType")
                        .WithMany()
                        .HasForeignKey("FiendTypeId");

                    b.Navigation("FiendType");
                });

            modelBuilder.Entity("Discord_Bot.Database.Fight", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}