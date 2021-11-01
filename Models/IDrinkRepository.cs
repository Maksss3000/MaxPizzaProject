using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
   public interface IDrinkRepository
    {
        public IEnumerable<Size> Sizes { get; }

        public IEnumerable<Drink> GetDrinksBySize(long sizeId);
    }
}
