using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
  public interface ICategoryRepository

    {
        Category GetCategoryById(long catId);

        IEnumerable<Category> GetSpecificProductCategories(string productType);

        IEnumerable<string> GetAllExistedTypes();

        void AddCategory(Category category);
    }
}
