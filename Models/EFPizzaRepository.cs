﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public class EFPizzaRepository : IPizzaRepository
    {
        private PizzeriaDbContext context;
        public EFPizzaRepository(PizzeriaDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Pizza> Pizzas => context.Pizzas.Include(c => c.Category).
                ThenInclude(s => s.CategoriesSizes).ThenInclude(s => s.Size);

       
        public IEnumerable<Pizza> GetPizzasByCategoryName(string catName)
        {
            return context.Pizzas.Include(c => c.Category).
               ThenInclude(s => s.CategoriesSizes).ThenInclude(s => s.Size).Where(c => c.Category.Name == catName);
        }
        public IEnumerable<Category> GetAllPizzasCategories()
        {
              return context.Categories.Where(c => c.Type == "Pizza");
         
        }

        public Pizza GetPizzaById(long id)
        {
          return  context.Pizzas.Include(c => c.Category).
                ThenInclude(s => s.CategoriesSizes).ThenInclude(s => s.Size).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Pizza> GetPizzasByCategoryId (long catId)
        {
            return context.Pizzas.Include(c => c.Category).
                ThenInclude(s => s.CategoriesSizes).ThenInclude(s => s.Size).Where(c=>c.Category.Id==catId);
        }

        public decimal GetPizzaPrice(long sizeId,long categoryId)
        {
            return context.CategoriesSizes.
                  Where(s => s.SizeId == sizeId).
                  Where(c => c.CategoryId == categoryId).Select(p => p.Price).FirstOrDefault();
            //p.Category.CategoriesSizes.
              //      Where(s => s.SizeId == sizeId).
                //    Where(c => c.CategoryId == p.CategoryId).Select(p => p.Price).FirstOrDefault();
        }

        public void AddPizza(Pizza p)
        {
            context.Products.Add(p);
            context.SaveChanges();
        }

        public void EditPizza(Pizza p)
        {
            context.Pizzas.Update(p);
            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public Product GetProduct(long prodId)
        {
            return context.Products.FirstOrDefault(p => p.Id == prodId);
        }
    }
}
