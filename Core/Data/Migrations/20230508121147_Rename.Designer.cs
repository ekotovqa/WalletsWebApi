﻿// <auto-generated />
using System;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230508121147_Rename")]
    partial class Rename
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Models.TmpBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Balance")
                        .HasColumnType("decimal(40,20)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("WalletId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WalletId")
                        .IsUnique();

                    b.ToTable("TmpBalances");
                });

            modelBuilder.Entity("Core.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Wallets", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Core.Models.TmpBalance", b =>
                {
                    b.HasOne("Core.Models.Wallet", "Wallets")
                        .WithOne("TmpBalance")
                        .HasForeignKey("Core.Models.TmpBalance", "WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("Core.Models.Wallet", b =>
                {
                    b.Navigation("TmpBalance")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
