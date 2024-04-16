﻿// <auto-generated />
using System;
using LogisticHub.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogisticHub.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240415060458_AddCreatedAtForOrder")]
    partial class AddCreatedAtForOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LogisticHub.Entities.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ClientEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("client_email");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long?>("TankerId")
                        .HasColumnType("bigint")
                        .HasColumnName("tanker_id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("TankerId")
                        .HasDatabaseName("ix_orders_tanker_id");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("LogisticHub.Entities.Tanker", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("FreeVolume")
                        .HasColumnType("numeric")
                        .HasColumnName("free_volume");

                    b.HasKey("Id")
                        .HasName("pk_tankers");

                    b.ToTable("tankers", (string)null);
                });

            modelBuilder.Entity("LogisticHub.Entities.Order", b =>
                {
                    b.HasOne("LogisticHub.Entities.Tanker", "Tanker")
                        .WithMany("Orders")
                        .HasForeignKey("TankerId")
                        .HasConstraintName("fk_orders_tankers_tanker_id");

                    b.Navigation("Tanker");
                });

            modelBuilder.Entity("LogisticHub.Entities.Tanker", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
