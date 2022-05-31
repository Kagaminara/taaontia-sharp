﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaaontiaCore.Database;

namespace TaaontiaCore.Migrations
{
    [DbContext(typeof(TaaontiaEntities))]
    partial class TaaontiaEntitiesModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("FiendSkill", b =>
                {
                    b.Property<Guid>("FiendsId")
                        .HasColumnType("TEXT");

                    b.Property<uint>("SkillsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FiendsId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("FiendSkill");
                });

            modelBuilder.Entity("FiendTypeSkill", b =>
                {
                    b.Property<ulong>("FiendTypesId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("SkillsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FiendTypesId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("FiendTypeSkill");
                });

            modelBuilder.Entity("PlayerSkill", b =>
                {
                    b.Property<Guid>("PlayersId")
                        .HasColumnType("TEXT");

                    b.Property<uint>("SkillsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlayersId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("PlayerSkill");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FightId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TargetId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FightId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TargetId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Fiend", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Energy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Experience")
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

                    b.ToTable("Fiend");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.FiendType", b =>
                {
                    b.Property<ulong>("Id")
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

                    b.Property<int>("Energy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Experience")
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

                    b.Property<ulong>("RemoteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Skill", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BaseSourceDamage")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BaseTargetDamage")
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

                    b.Property<uint?>("StatusTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StatusTypeId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.StatusType", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BaseValue")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Effect")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StatusType");
                });

            modelBuilder.Entity("FiendSkill", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Fiend", null)
                        .WithMany()
                        .HasForeignKey("FiendsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaaontiaCore.Database.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FiendTypeSkill", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.FiendType", null)
                        .WithMany()
                        .HasForeignKey("FiendTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaaontiaCore.Database.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlayerSkill", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaaontiaCore.Database.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Event", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Fight", "Fight")
                        .WithMany()
                        .HasForeignKey("FightId");

                    b.HasOne("TaaontiaCore.Database.Models.Player", "Player")
                        .WithMany("Events")
                        .HasForeignKey("PlayerId");

                    b.HasOne("TaaontiaCore.Database.Models.Fiend", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId");

                    b.Navigation("Fight");

                    b.Navigation("Player");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Fight", b =>
                {
                    b.HasOne("TaaontiaCore.Database.Models.Fiend", "Fiend")
                        .WithMany("Fights")
                        .HasForeignKey("FiendId");

                    b.HasOne("TaaontiaCore.Database.Models.Player", "Player")
                        .WithMany("Fights")
                        .HasForeignKey("PlayerId");

                    b.Navigation("Fiend");

                    b.Navigation("Player");
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
                    b.HasOne("TaaontiaCore.Database.Models.StatusType", "StatusType")
                        .WithMany()
                        .HasForeignKey("StatusTypeId");

                    b.Navigation("StatusType");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Fiend", b =>
                {
                    b.Navigation("Fights");
                });

            modelBuilder.Entity("TaaontiaCore.Database.Models.Player", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Fights");
                });
#pragma warning restore 612, 618
        }
    }
}
