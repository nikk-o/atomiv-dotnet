﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Optivem.Template.Infrastructure.EntityFrameworkCore;

namespace Optivem.Template.Infrastructure.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Customers.CustomerRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderDetailRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte?>("OrderDetailStatusRecordId");

                    b.Property<int>("OrderRecordId")
                        .HasColumnName("order_id");

                    b.Property<int>("ProductRecordId")
                        .HasColumnName("product_id");

                    b.Property<decimal>("Quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StatusRecordId")
                        .HasColumnName("status_id");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnName("unit_price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderDetailStatusRecordId");

                    b.HasIndex("OrderRecordId");

                    b.HasIndex("ProductRecordId");

                    b.ToTable("order_detail");
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderDetailStatusRecord", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("order_detail_status");

                    b.HasData(
                        new
                        {
                            Id = (byte)0,
                            Code = "None"
                        },
                        new
                        {
                            Id = (byte)1,
                            Code = "Allocated"
                        },
                        new
                        {
                            Id = (byte)2,
                            Code = "Invoiced"
                        },
                        new
                        {
                            Id = (byte)3,
                            Code = "Shipped"
                        },
                        new
                        {
                            Id = (byte)4,
                            Code = "OnOrder"
                        },
                        new
                        {
                            Id = (byte)5,
                            Code = "NoStock"
                        });
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerRecordId")
                        .HasColumnName("customer_id");

                    b.Property<byte>("OrderStatusRecordId")
                        .HasColumnName("status_id");

                    b.HasKey("Id");

                    b.HasIndex("CustomerRecordId");

                    b.HasIndex("OrderStatusRecordId");

                    b.ToTable("order");
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderStatusRecord", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("order_status");

                    b.HasData(
                        new
                        {
                            Id = (byte)0,
                            Code = "None"
                        },
                        new
                        {
                            Id = (byte)1,
                            Code = "New"
                        },
                        new
                        {
                            Id = (byte)2,
                            Code = "Invoiced"
                        },
                        new
                        {
                            Id = (byte)3,
                            Code = "Shipped"
                        },
                        new
                        {
                            Id = (byte)4,
                            Code = "Closed"
                        },
                        new
                        {
                            Id = (byte)7,
                            Code = "Submitted"
                        },
                        new
                        {
                            Id = (byte)8,
                            Code = "Cancelled"
                        },
                        new
                        {
                            Id = (byte)9,
                            Code = "Archived"
                        });
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Products.ProductRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnName("active");

                    b.Property<decimal>("ListPrice")
                        .HasColumnName("list_price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasMaxLength(10);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("product");
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderDetailRecord", b =>
                {
                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderDetailStatusRecord", "OrderDetailStatusRecord")
                        .WithMany("OrderDetailRecords")
                        .HasForeignKey("OrderDetailStatusRecordId");

                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderRecord", "OrderRecord")
                        .WithMany("OrderDetailRecords")
                        .HasForeignKey("OrderRecordId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Products.ProductRecord", "ProductRecord")
                        .WithMany("OrderDetailRecords")
                        .HasForeignKey("ProductRecordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderRecord", b =>
                {
                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Customers.CustomerRecord", "CustomerRecord")
                        .WithMany("OrderRecords")
                        .HasForeignKey("CustomerRecordId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderStatusRecord", "OrderStatusRecord")
                        .WithMany("OrderRecords")
                        .HasForeignKey("OrderStatusRecordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}