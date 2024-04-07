﻿// <auto-generated />

using ManagerAccount.Repositories.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManagerAccount.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240403055547_AddOrdersReference")]
    partial class AddOrdersReference
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ManagerAccount.Entities.OrderToManager", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ManagerId")
                        .HasColumnType("bigint")
                        .HasColumnName("manager_id");

                    b.HasKey("Id")
                        .HasName("pk_order_to_manager");

                    b.HasIndex("ManagerId")
                        .HasDatabaseName("ix_order_to_manager_manager_id");

                    b.ToTable("order_to_manager", (string)null);
                });

            modelBuilder.Entity("ManagerAccount.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Client")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("ManagerAccount.Entities.Client", b =>
                {
                    b.HasBaseType("ManagerAccount.Entities.User");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.ToTable("clients", (string)null);
                });

            modelBuilder.Entity("ManagerAccount.Entities.Manager", b =>
                {
                    b.HasBaseType("ManagerAccount.Entities.User");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.ToTable("managers", (string)null);
                });

            modelBuilder.Entity("ManagerAccount.Entities.OrderToManager", b =>
                {
                    b.HasOne("ManagerAccount.Entities.Manager", "Manager")
                        .WithMany("OrderIds")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_to_manager_managers_manager_id");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("ManagerAccount.Entities.Client", b =>
                {
                    b.HasOne("ManagerAccount.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("ManagerAccount.Entities.Client", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_clients_users_id");
                });

            modelBuilder.Entity("ManagerAccount.Entities.Manager", b =>
                {
                    b.HasOne("ManagerAccount.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("ManagerAccount.Entities.Manager", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_managers_users_id");
                });

            modelBuilder.Entity("ManagerAccount.Entities.Manager", b =>
                {
                    b.Navigation("OrderIds");
                });
#pragma warning restore 612, 618
        }
    }
}