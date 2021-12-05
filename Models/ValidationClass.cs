using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace MaxPizzaProject.Models
{
    public class ValidationClass
    {
        private ISizeRepository sizeRepo;
        private ICategoryRepository catRepo;
        public ValidationClass(ISizeRepository sizeRep,ICategoryRepository catRep)
        {
            sizeRepo = sizeRep;
            catRepo = catRep;
        }
       
        public void SizeNameValidation (ModelStateDictionary ModelState,Size size)
        {
            if (string.IsNullOrEmpty(size.TheSize))
            {
                ModelState.AddModelError(string.Empty, "Please enter size");
            }

            if (sizeRepo.GetSizeBySizeName(size.TheSize) != null)
            {
                ModelState.AddModelError(string.Empty,
                                         "This size is already existed, enter another one.");
            }
        }


        public Category ProductValidation(ModelStateDictionary ModelState ,Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError(nameof(product.Name), "Please Enter Product name");

            }

            Category category = catRepo.GetCategoryById(product.CategoryId);

            if (category == null)
            {
                ModelState.AddModelError(nameof(product.Category), "Please choose Product Category");
            }

            if (string.IsNullOrEmpty(product.Description))
            {
                ModelState.AddModelError(nameof(product.Description), "Please enter Product Discription");
            }

            return category;
        }


        public void CategoryValidation(ModelStateDictionary ModelState,Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                ModelState.AddModelError(nameof(category.Name), "Please Enter Category name");

            }

            if (string.IsNullOrEmpty(category.Type))
            {
                ModelState.AddModelError(nameof(category.Type), "Please choose Type for Category");
            }
        }


        public void SizePriceValidation(ModelStateDictionary ModelState,CategorySize categorySize)
        {
            if (categorySize.Size == null)
            {
                ModelState.AddModelError(nameof(categorySize.Size.TheSize),
                                               "Please choose Size");
            }

            if (categorySize.Price <= 0)
            {
                ModelState.AddModelError(nameof(categorySize.Price),
                                         "Please enter positive price");
            }
        }

     
        public void AddNewSizeValidation(ModelStateDictionary ModelState,
                                         string sizeName,decimal sizePrice)
        {
            if (string.IsNullOrEmpty(sizeName))
            {
                ModelState.AddModelError(string.Empty, "Please choose size to add");
            }
            if (sizePrice <= 0)
            {
                ModelState.AddModelError(string.Empty, "Please enter positive price for size");
            }
        }

        public void CategoryDeletingValidation(ModelStateDictionary ModelState,Category cat)
        {
            if (catRepo.GetSpecificProductCategories(cat.Type).Count() == 1)
            {
                ModelState.AddModelError(string.Empty,
                                        "You can`t Delete last existed Category of this type.");
            }
        }

        public void PriceValidation(ModelStateDictionary ModelState , decimal price)
        {
            if (price <= 0)
            {
                ModelState.AddModelError(string.Empty, "Please enter Positive Price");
            }
        }

        public void CategoryNameValidation (ModelStateDictionary ModelState,Category category,string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError(nameof(category.Name), "Please enter category name");

            }
        }
    }
}
