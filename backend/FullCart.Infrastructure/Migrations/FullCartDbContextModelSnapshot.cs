﻿// <auto-generated />
using System;
using FullCart.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FullCart.Infrastructure.Migrations
{
    [DbContext(typeof(FullCartDbContext))]
    partial class FullCartDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FullCart.Domain.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AddedQuantity")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("InventoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId")
                        .IsUnique()
                        .HasFilter("[InventoryId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("InventoryStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(20, 2)");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("InventoryStatusId");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.InventoryStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("StatusDesc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InventoryStatus");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("OrderPrice")
                        .HasColumnType("decimal(20, 2)");

                    b.Property<Guid?>("OrderStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ShippingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.OrderStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("StatusDesc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.Cart", b =>
                {
                    b.HasOne("FullCart.Domain.Entities.Inventory", "InventoryItem")
                        .WithOne("Cart")
                        .HasForeignKey("FullCart.Domain.Entities.Cart", "InventoryId");

                    b.HasOne("FullCart.Domain.Entities.User", "CartUser")
                        .WithMany("UserCart")
                        .HasForeignKey("UserId");

                    b.Navigation("CartUser");

                    b.Navigation("InventoryItem");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.Inventory", b =>
                {
                    b.HasOne("FullCart.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("FullCart.Domain.Entities.InventoryStatus", "InventoryStatus")
                        .WithMany()
                        .HasForeignKey("InventoryStatusId");

                    b.Navigation("Category");

                    b.Navigation("InventoryStatus");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.Order", b =>
                {
                    b.HasOne("FullCart.Domain.Entities.OrderStatus", "OrderStatus")
                        .WithMany("Order")
                        .HasForeignKey("OrderStatusId");

                    b.HasOne("FullCart.Domain.Entities.User", "UserOrder")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("OrderStatus");

                    b.Navigation("UserOrder");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("FullCart.Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FullCart.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.Inventory", b =>
                {
                    b.Navigation("Cart");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.OrderStatus", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("FullCart.Domain.Entities.User", b =>
                {
                    b.Navigation("UserCart");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
