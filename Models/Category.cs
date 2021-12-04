using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public class Category
    {
      
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }
        public  IEnumerable<Size> Sizes { get; set; } 

        public List<CategorySize> CategoriesSizes { get; set; } = new List<CategorySize>();

        public IEnumerable<Pizza> Pizzas { get; set; }

        public IEnumerable<Topping> Toppings { get; set; }

        public IEnumerable<Snack> Snacks { get; set; }
       
    }
}
