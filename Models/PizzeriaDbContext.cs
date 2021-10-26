using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public class PizzeriaDbContext : DbContext
    {
        
        public PizzeriaDbContext(DbContextOptions<PizzeriaDbContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Pizza> Pizzas { get; set; } 

        public DbSet<Category> Categories { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<Topping> Toppings { get; set; }

        
        public DbSet<CategorySize> CategoriesSizes { get; set; }

        //public DbSet<Price> Prices { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*
            modelBuilder.Entity<Category>().HasMany(x => x.Sizes)
               .WithMany(x => x.Categories)
               .UsingEntity<CategorySize>(
                   x => x.HasOne(x => x.Size)
                   .WithMany().HasForeignKey(x => x.SizeId),
                   x => x.HasOne(x => x.Category)
                  .WithMany().HasForeignKey(x => x.CategoryId));
            */


            modelBuilder.Entity<Category>().HasMany(x => x.Sizes)
               .WithMany(x => x.Categories)
               .UsingEntity<CategorySize>
                 (j => j.HasOne(m => m.Size).WithMany(c => c.CategoriesSizes),
                 j => j.HasOne(m => m.Category).WithMany(c => c.CategoriesSizes));


            
        }
    }



}
        
    

