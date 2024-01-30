﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TianLiUpdate.API.Data;

#nullable disable

namespace TianLiUpdate.API.Migrations
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20240130224808_newChanged")]
    partial class newChanged
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("TianLiUpdate.API.Models.File", b =>
                {
                    b.Property<Guid>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DownloadUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProjectVersionId")
                        .HasColumnType("TEXT");

                    b.HasKey("FileId");

                    b.HasIndex("ProjectVersionId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("TianLiUpdate.API.Models.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateTokenId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TianLiUpdate.API.Models.ProjectVersion", b =>
                {
                    b.Property<Guid>("ProjectVersionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreateTokenId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DownloadUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDraft")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdateLog")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProjectVersionId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("TianLiUpdate.API.Models.Token", b =>
                {
                    b.Property<Guid>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUseTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("TokenString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TokenId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("TianLiUpdate.API.Models.File", b =>
                {
                    b.HasOne("TianLiUpdate.API.Models.ProjectVersion", "ProjectVersion")
                        .WithMany("Files")
                        .HasForeignKey("ProjectVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectVersion");
                });

            modelBuilder.Entity("TianLiUpdate.API.Models.ProjectVersion", b =>
                {
                    b.HasOne("TianLiUpdate.API.Models.Project", "Project")
                        .WithMany("Versions")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TianLiUpdate.API.Models.Project", b =>
                {
                    b.Navigation("Versions");
                });

            modelBuilder.Entity("TianLiUpdate.API.Models.ProjectVersion", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
