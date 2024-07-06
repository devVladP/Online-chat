﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineChat.Persistence;

#nullable disable

namespace OnlineChat.Persistence.Migrations
{
    [DbContext(typeof(OnlineChatDbContext))]
    [Migration("20240706191241_Init-Migration")]
    partial class InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("online-chatdb")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineChat.Core.Domain.Groups.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Groups", "online-chatdb");
                });

            modelBuilder.Entity("OnlineChat.Core.Domain.Groups.Models.UserGroup", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroup", "online-chatdb");
                });

            modelBuilder.Entity("OnlineChat.Core.Domain.Messages.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Messages", "online-chatdb");
                });

            modelBuilder.Entity("OnlineChat.Core.Domain.Users.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Users", "online-chatdb");
                });

            modelBuilder.Entity("OnlineChat.Core.Domain.Groups.Models.Group", b =>
                {
                    b.HasOne("OnlineChat.Core.Domain.Users.Models.User", "Owner")
                        .WithMany("GroupsOwner")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("OnlineChat.Core.Domain.Groups.Models.UserGroup", b =>
                {
                    b.HasOne("OnlineChat.Core.Domain.Groups.Models.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OnlineChat.Core.Domain.Users.Models.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineChat.Core.Domain.Messages.Models.Message", b =>
                {
                    b.HasOne("OnlineChat.Core.Domain.Groups.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("OnlineChat.Core.Domain.Groups.Models.Group", b =>
                {
                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("OnlineChat.Core.Domain.Users.Models.User", b =>
                {
                    b.Navigation("GroupsOwner");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
