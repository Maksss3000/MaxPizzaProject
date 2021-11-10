using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
   public interface IDrinkRepository
    {
         IEnumerable<string> Sizes { get; }

         long GetSizeIdBySizeName(string sizeName);
         IEnumerable<Drink> GetDrinksBySize(long sizeId);
        void AddDrink(Drink drink);


    }
}
