using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public class EFToppingRepository :IToppingRepository
    {
        private PizzeriaDbContext context;

        public EFToppingRepository(PizzeriaDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Topping> Toppings => context.Toppings.Include(c => c.Category).
                ThenInclude(s => s.CategoriesSizes).ThenInclude(s => s.Size);

        
        public IEnumerable<long> GetToppingCategoriesIds()
        {
           
            IEnumerable<long> toppIds = context.Categories.Where(t => t.Type == "Topping").Select(i => i.Id);
        
            return toppIds;
        }

        public IEnumerable<Topping> GetToppingsByCategory(long catId)
        {
            return context.Toppings.Where(t => t.CategoryId == catId);
        }


        public IEnumerable<Category> GetToppCategories()
        {
            return context.Categories.Where(c => c.Type == "Topping");
        }
        public Dictionary<long, decimal> GetToppingPrice(IEnumerable<long> toppIds,long sizeId)
        {

            Dictionary<long, decimal> priceDict = new Dictionary<long, decimal>();

            foreach (long catId in toppIds)
            {

                
                decimal price = context.CategoriesSizes.Where(c => c.CategoryId == catId).
                    Where(s => s.SizeId == sizeId).
                    Select(p => p.Price).FirstOrDefault();

                priceDict.Add(catId, price);
            }

            return priceDict;
        }

        public decimal GetToppPrice(long toppCatId, long sizeId)
        {
            decimal price = context.CategoriesSizes.Where(c => c.CategoryId == toppCatId).
                     Where(s => s.SizeId == sizeId).
                     Select(p => p.Price).FirstOrDefault();

            return price;
        }
    }
}

