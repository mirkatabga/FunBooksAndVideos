// <auto-generated />
using System;
using FunBooksAndVideos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FunBooksAndVideos.Infrastructure.Migrations
{
    [DbContext(typeof(FunBooksAndVideosContext))]
    [Migration("20230131154102_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomerProduct", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomerId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CustomerProduct", (string)null);
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MembershipId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MembershipId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.Membership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeliveryAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MembershipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MembershipId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.DigitalProduct", b =>
                {
                    b.HasBaseType("FunBooksAndVideos.Domain.Product");

                    b.Property<double>("SizeInMB")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("DigitalProduct");
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.PhysicalProduct", b =>
                {
                    b.HasBaseType("FunBooksAndVideos.Domain.Product");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("PhysicalProduct");
                });

            modelBuilder.Entity("CustomerProduct", b =>
                {
                    b.HasOne("FunBooksAndVideos.Domain.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FunBooksAndVideos.Domain.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.Customer", b =>
                {
                    b.HasOne("FunBooksAndVideos.Domain.Membership", "Membership")
                        .WithMany()
                        .HasForeignKey("MembershipId");

                    b.Navigation("Membership");
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.Order", b =>
                {
                    b.HasOne("FunBooksAndVideos.Domain.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.OrderItem", b =>
                {
                    b.HasOne("FunBooksAndVideos.Domain.Membership", "Membership")
                        .WithMany()
                        .HasForeignKey("MembershipId");

                    b.HasOne("FunBooksAndVideos.Domain.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("FunBooksAndVideos.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Membership");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FunBooksAndVideos.Domain.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
