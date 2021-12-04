using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public class EFSnackRepository : ISnackRepository
    {
        private PizzeriaDbContext context;
        public EFSnackRepository(PizzeriaDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Snack> Snacks => context.Snacks.Include(c => c.Category).
                ThenInclude(s => s.CategoriesSizes).ThenInclude(s => s.Size);

        public IEnumerable<Category> GetAllSnackCategories()
        {
            return context.Categories.Where(c => c.Type == "Snack");
        }

        public Snack GetSnackById(long id)
        {
            return context.Snacks.Include(c => c.Category).
                ThenInclude(s => s.CategoriesSizes).ThenInclude(s => s.Size).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Snack> GetSnacksByCategoryName(string catName)
        {
            return context.Snacks.Include(c => c.Category).
               ThenInclude(s => s.CategoriesSizes).ThenInclude(s => s.Size).Where(c => c.Category.Name == catName);
        }
    }
}
