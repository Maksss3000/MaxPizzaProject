using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private PizzeriaDbContext context;

      public EFCategoryRepository (PizzeriaDbContext ctx)

        {
            context = ctx;
        }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }
        public IEnumerable<string> GetAllExistedTypes()
        {
            return context.Categories.Select(c => c.Type).Distinct();
            
        }

        public Category GetCategoryById(long catId)
        {
          return  context.Categories.Include(s=>s.Sizes).FirstOrDefault(c=>c.Id==catId);
        }


        public IEnumerable<Category> GetSpecificProductCategories(string productType)
        {
            return context.Categories.Include(c => c.CategoriesSizes).Include(s => s.Sizes).Where(c => c.Type == productType);
           
        }

        public IEnumerable<Product> GetProductsOfSpecificCategory(long catId)
        {
           
            return context.Products.Where(c => c.CategoryId == catId);
        }

        public void RemoveCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public CategorySize GetSpecificCatSize(long catId, long sizeId)
        {
            return context.CategoriesSizes.Where(c => c.CategoryId == catId && c.SizeId == sizeId).FirstOrDefault();
        }
    }
}
