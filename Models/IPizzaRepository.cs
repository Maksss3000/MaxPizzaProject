﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public interface IPizzaRepository
    {
        public IEnumerable<Pizza> Pizzas { get; }
        public Pizza GetPizzaById(long id);

        public IEnumerable<Pizza> GetPizzasByCategoryName(string catName);
        public IEnumerable<Pizza> GetPizzasByCategoryId(long id);
        public decimal GetPizzaPrice(long sizeId, long categoryId);

        public IEnumerable<Category> GetAllPizzasCategories();

        Product GetProduct(long prodId);
        public void EditPizza(Pizza p);
        
        public void AddPizza(Pizza p);

        void DeleteProduct(Product product);
    }
}
