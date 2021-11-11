using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public interface IToppingRepository
    {
        IEnumerable<Topping> Toppings { get; }
        public long GetCategoryIdByToppingName(string toppName);

        public IEnumerable<long> GetToppingCategoriesIds();

        public Dictionary<long, decimal> GetToppingPrice (IEnumerable<long> ids, long sizeId);

        public decimal GetToppPrice(long toppCatId, long sizeId);
        public IEnumerable<Category> GetToppCategories();

        public IEnumerable<Topping> GetToppingsByCategory(long catId);
         void AddTopping(Topping topping);

        void EditTopping(Topping topping);
    }
}
