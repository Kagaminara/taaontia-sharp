﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaaontiaCore.Database;

namespace TaaontiaCore.Migrations
{
    [DbContext(typeof(TaaontiaEntities))]
    [Migration("20211126224402_SkillsDescription")]
    partial class SkillsDescription
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("CharacterSkill", b =>
                {
                    b.Property<Guid>("CharactersId")
                        .HasColumnType("TEXT");

                    b.Property<uint>("SkillsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CharactersId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("CharacterSkill");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Energy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Experience")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("FightId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FightId1")
                        .HasColumnType("TEXT");

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

                    b.HasIndex("FightId");

                    b.HasIndex("FightId1");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FightId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SourceIdId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TargetId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FightId");

                    b.HasIndex("SourceIdId");

                    b.HasIndex("TargetId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Fiend", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CharacterForeignKey")
                        .HasColumnType("TEXT");

                    b.Property<uint?>("FiendTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CharacterForeignKey")
                        .IsUnique();

                    b.HasIndex("FiendTypeId");

                    b.ToTable("Fiend");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.FiendType", b =>
                {
                    b.Property<uint>("Id")
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

            modelBuilder.Entity("TaaontiaCore.Database.Models.Fight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FiendId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsGlobal")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FiendId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Fight");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CharacterForeignKey")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CharacterForeignKey")
                        .IsUnique();

                    b.ToTable("Player");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Skill", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaseSourceDamage")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaseTargetDamage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<uint?>("SourceStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<uint?>("TargetStatusId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SourceStatusId");

                    b.HasIndex("TargetStatusId");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("RemainingDuration")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("SourceId")
                        .HasColumnType("TEXT");

                    b.Property<uint?>("StatusTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("TargetId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.HasIndex("StatusTypeId");

                    b.HasIndex("TargetId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.StatusType", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaseValue")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Effect")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StatusType");
                });

            modelBuilder.Entity("CharacterSkill", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaaontiaCore.Database.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Character", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Fight", null)
                        .WithMany("Allies")
                        .HasForeignKey("FightId");

                    b.HasOne("TaaontiaCore.Database.Models.Fight", null)
                        .WithMany("Fiends")
                        .HasForeignKey("FightId1");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Event", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Fight", "Fight")
                        .WithMany("Events")
                        .HasForeignKey("FightId");

                    b.HasOne("TaaontiaCore.Database.Models.Character", "SourceId")
                        .WithMany()
                        .HasForeignKey("SourceIdId");

                    b.HasOne("TaaontiaCore.Database.Models.Character", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId");

                    b.Navigation("Fight");

                    b.Navigation("SourceId");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Fiend", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Character", "Character")
                        .WithOne("Fiend")
                        .HasForeignKey("TaaontiaCore.Database.Models.Fiend", "CharacterForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaaontiaCore.Database.Models.FiendType", "FiendType")
                        .WithMany()
                        .HasForeignKey("FiendTypeId");

                    b.Navigation("Character");

                    b.Navigation("FiendType");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Fight", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Fiend", null)
                        .WithMany("Fights")
                        .HasForeignKey("FiendId");

                    b.HasOne("TaaontiaCore.Database.Models.Player", null)
                        .WithMany("Fights")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Player", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Character", "Character")
                        .WithOne("Player")
                        .HasForeignKey("TaaontiaCore.Database.Models.Player", "CharacterForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Skill", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.StatusType", "SourceStatus")
                        .WithMany()
                        .HasForeignKey("SourceStatusId");

                    b.HasOne("TaaontiaCore.Database.Models.StatusType", "TargetStatus")
                        .WithMany()
                        .HasForeignKey("TargetStatusId");

                    b.Navigation("SourceStatus");

                    b.Navigation("TargetStatus");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Status", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Character", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId");

                    b.HasOne("TaaontiaCore.Database.Models.StatusType", "StatusType")
                        .WithMany()
                        .HasForeignKey("StatusTypeId");

                    b.HasOne("TaaontiaCore.Database.Models.Character", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId");

                    b.Navigation("Source");

                    b.Navigation("StatusType");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Character", b =>
                {
                    b.Navigation("Fiend");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Fiend", b =>
                {
                    b.Navigation("Fights");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Fight", b =>
                {
                    b.Navigation("Allies");

                    b.Navigation("Events");

                    b.Navigation("Fiends");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Player", b =>
                {
                    b.Navigation("Fights");
                });
#pragma warning restore 612, 618
        }
    }
}
