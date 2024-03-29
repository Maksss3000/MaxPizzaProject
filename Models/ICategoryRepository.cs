﻿using System;
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

        IEnumerable<Product> GetProductsOfSpecificCategory(long catId);

        void RemoveCategory(Category category);
        void AddCategory(Category category);
        void UpdateCategory(Category category);

        CategorySize GetSpecificCatSize(long catId,long sizeId);
       
    }
}
