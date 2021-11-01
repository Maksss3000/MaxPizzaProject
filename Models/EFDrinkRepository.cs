using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public class EFDrinkRepository : IDrinkRepository
    {
        private PizzeriaDbContext context;
        public EFDrinkRepository(PizzeriaDbContext ctx)
        {
            context = ctx;
        }

         public IEnumerable<Size> Sizes => context.CategoriesSizes.
           Include(s=>s.Size).Where(s=>s.Category.Type=="Drink").Select(s=>s.Size);

        public IEnumerable<Drink> GetDrinksBySize(long sizeId)
        {
            return context.Drinks.
                Include(c => c.Category).
                ThenInclude(s => s.CategoriesSizes.Where(s => s.SizeId == sizeId));
        }
    }
}
