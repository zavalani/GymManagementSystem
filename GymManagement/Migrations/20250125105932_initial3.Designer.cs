﻿// <auto-generated />
using System;
using GymManagementx;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GymManagement.Migrations
{
    [DbContext(typeof(GymDbContext))]
    [Migration("20250125105932_initial3")]
    partial class initial3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GymManagement.Models.Discounted_Member_Subscriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DiscountsId")
                        .HasColumnType("int");

                    b.Property<int?>("Member_SubscriptionsId")
                        .HasColumnType("int");

                    b.Property<int>("Members_SubscriptionsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiscountsId");

                    b.HasIndex("Member_SubscriptionsId");

                    b.ToTable("Discounted_Member_Subscriptions");
                });

            modelBuilder.Entity("GymManagement.Models.Discounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("GymManagement.Models.Member_Subscriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("DiscountValue")
                        .HasColumnType("float");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MembersId")
                        .HasColumnType("int");

                    b.Property<double>("OriginalPrice")
                        .HasColumnType("float");

                    b.Property<double>("PaidPrice")
                        .HasColumnType("float");

                    b.Property<int>("RemainingSessions")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubscriptionsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MembersId");

                    b.HasIndex("SubscriptionsId");

                    b.ToTable("Member_Subscriptions");
                });

            modelBuilder.Entity("GymManagement.Models.Members", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdCardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("GymManagement.Models.Subscriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("NumOfMonths")
                        .HasColumnType("int");

                    b.Property<int>("TotalNumSessions")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("WeekFrequency")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("GymManagement.Models.Discounted_Member_Subscriptions", b =>
                {
                    b.HasOne("GymManagement.Models.Discounts", "Discounts")
                        .WithMany()
                        .HasForeignKey("DiscountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymManagement.Models.Member_Subscriptions", "Member_Subscriptions")
                        .WithMany()
                        .HasForeignKey("Member_SubscriptionsId");

                    b.Navigation("Discounts");

                    b.Navigation("Member_Subscriptions");
                });

            modelBuilder.Entity("GymManagement.Models.Member_Subscriptions", b =>
                {
                    b.HasOne("GymManagement.Models.Members", "Members")
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymManagement.Models.Subscriptions", "Subscriptions")
                        .WithMany()
                        .HasForeignKey("SubscriptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Members");

                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
