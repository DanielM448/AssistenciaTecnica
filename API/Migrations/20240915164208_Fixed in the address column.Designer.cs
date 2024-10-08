﻿// <auto-generated />
using System;
using API.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20240915164208_Fixed in the address column")]
    partial class Fixedintheaddresscolumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Models.AddressModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("city");

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("client_id");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("complement");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("neighborhood");

                    b.Property<int>("Number")
                        .HasColumnType("int")
                        .HasColumnName("number");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("state");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Street");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("zip_code");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("enderecos");
                });

            modelBuilder.Entity("Models.ClientModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CellNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("cell_number");

                    b.Property<string>("CellNumberAlternative")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("cell_number_alternative");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("clients");
                });

            modelBuilder.Entity("Models.EquipmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("brand");

                    b.Property<string>("ModelEquipment")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("model");

                    b.Property<string>("ProblemDescription")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("problem_description");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("serial_number");

                    b.HasKey("Id");

                    b.ToTable("equipments");
                });

            modelBuilder.Entity("Models.PartModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<int>("OsId")
                        .HasColumnType("int")
                        .HasColumnName("os_id");

                    b.Property<double>("Price")
                        .HasColumnType("double")
                        .HasColumnName("price");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("OsId");

                    b.ToTable("parts");
                });

            modelBuilder.Entity("Models.RoleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Tecnichian"
                        },
                        new
                        {
                            Id = 3,
                            RoleName = "Client"
                        });
                });

            modelBuilder.Entity("Models.ServiceOrderModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("client_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int")
                        .HasColumnName("equipment_id");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("service_orders");
                });

            modelBuilder.Entity("Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("access_token");

                    b.Property<DateTime>("Deleted")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("deleted");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("user_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("password");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("refresh_token_expiry_time");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Models.UserRoleModel", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Models.AddressModel", b =>
                {
                    b.HasOne("Models.ClientModel", "Client")
                        .WithMany("Addresses")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Models.PartModel", b =>
                {
                    b.HasOne("Models.ServiceOrderModel", "Order")
                        .WithMany("Parts")
                        .HasForeignKey("OsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Models.ServiceOrderModel", b =>
                {
                    b.HasOne("Models.ClientModel", "Client")
                        .WithMany("ServiceOrders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.EquipmentModel", "Equipment")
                        .WithMany("ServiceOrders")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("Models.UserRoleModel", b =>
                {
                    b.HasOne("Models.RoleModel", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.UserModel", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.ClientModel", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("ServiceOrders");
                });

            modelBuilder.Entity("Models.EquipmentModel", b =>
                {
                    b.Navigation("ServiceOrders");
                });

            modelBuilder.Entity("Models.RoleModel", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Models.ServiceOrderModel", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("Models.UserModel", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
