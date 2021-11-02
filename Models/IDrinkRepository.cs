using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
   public interface IDrinkRepository
    {
        public IEnumerable<string> Sizes { get; }

        public long GetSizeIdBySizeName(string sizeName);
        public IEnumerable<Drink> GetDrinksBySize(long sizeId);
    }
}
