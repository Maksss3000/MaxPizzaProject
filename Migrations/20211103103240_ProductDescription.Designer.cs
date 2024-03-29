﻿// <auto-generated />
using MaxPizzaProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MaxPizzaProject.Migrations
{
    [DbContext(typeof(PizzeriaDbContext))]
    [Migration("20211103103240_ProductDescription")]
    partial class ProductDescription
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MaxPizzaProject.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.CategorySize", b =>
                {
                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<long>("SizeId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CategoryId", "SizeId");

                    b.HasIndex("SizeId");

                    b.ToTable("CategoriesSizes");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InStock")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Size", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TheSize")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Drink", b =>
                {
                    b.HasBaseType("MaxPizzaProject.Models.Product");

                    b.HasIndex("CategoryId");

                    b.HasDiscriminator().HasValue("Drink");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Pizza", b =>
                {
                    b.HasBaseType("MaxPizzaProject.Models.Product");

                    b.HasIndex("CategoryId");

                    b.HasDiscriminator().HasValue("Pizza");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Topping", b =>
                {
                    b.HasBaseType("MaxPizzaProject.Models.Product");

                    b.HasIndex("CategoryId");

                    b.HasDiscriminator().HasValue("Topping");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.CategorySize", b =>
                {
                    b.HasOne("MaxPizzaProject.Models.Category", "Category")
                        .WithMany("CategoriesSizes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MaxPizzaProject.Models.Size", "Size")
                        .WithMany("CategoriesSizes")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Drink", b =>
                {
                    b.HasOne("MaxPizzaProject.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Pizza", b =>
                {
                    b.HasOne("MaxPizzaProject.Models.Category", "Category")
                        .WithMany("Pizzas")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Topping", b =>
                {
                    b.HasOne("MaxPizzaProject.Models.Category", "Category")
                        .WithMany("Toppings")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Category", b =>
                {
                    b.Navigation("CategoriesSizes");

                    b.Navigation("Pizzas");

                    b.Navigation("Toppings");
                });

            modelBuilder.Entity("MaxPizzaProject.Models.Size", b =>
                {
                    b.Navigation("CategoriesSizes");
                });
#pragma warning restore 612, 618
        }
    }
}
