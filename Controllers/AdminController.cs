using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace MaxPizzaProject.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {

        private PizzeriaDbContext context;
        private IPizzaRepository pizzaRepo;
        private ICategoryRepository catRepo;
        private IDrinkRepository drinkRepo;
        private IToppingRepository toppRepo;
        private ISizeRepository sizeRepo;

        public AdminController(PizzeriaDbContext ctx, IDrinkRepository drinkRep,
                               IPizzaRepository pizzaRep, ICategoryRepository catRep, IToppingRepository topRep, ISizeRepository sizRep)
        {
            context = ctx;
            pizzaRepo = pizzaRep;
            catRepo = catRep;
            drinkRepo = drinkRep;
            toppRepo = topRep;
            sizeRepo = sizRep;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult ChooseProductForAdding()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ChooseCategoryToEdit(string type)
        {
           
            return View(catRepo.GetSpecificProductCategories(type));
        }

        [HttpPost]
        public IActionResult RemoveCategory(long catId)
        {
           
            Category cat = catRepo.GetCategoryById(catId);
            if (catRepo.GetSpecificProductCategories(cat.Type).Count() == 1)
            {
                ModelState.AddModelError(string.Empty, 
                                        "You can`t Delete last existed Category of this type.");
            }
            if (ModelState.IsValid)
            {
                catRepo.RemoveCategory(cat);
            }
            
            return View("ChooseCategoryToEdit", catRepo.GetSpecificProductCategories(cat.Type));  
        }

        [HttpGet]
        public IActionResult EditCategory(long catId)
        {
            Category category = catRepo.GetCategoryById(catId);
            if (category == null)
            {
                return RedirectToAction("AllPizzas", "Home");
            }
            //Existed Sizes of specific category.
            IEnumerable<Size> sizesOfSpecCat = category.Sizes;
            //All sizes except of  sizes of specific category.
            IEnumerable<Size> exsSizes = sizeRepo.GetAllExistedSizes().Except(sizesOfSpecCat);
            if (exsSizes.Count() != 0)
            {
                ViewBag.ExistedSizes = exsSizes;
            }
            else
            {
                ViewBag.ExistedSizes = null;
            }

            /*
            IEnumerable<Product> products = catRepo.GetProductsOfSpecificCategory(category.Id);
            
            if (products.Count() !=0)
            {
                ViewBag.Products = products;
            }
            else
            {
                ViewBag.Products = null;
            }
            */
            GetProductsOfSpecCategory(catId);
            return View("EditCategoryForm", category);
        }

        [HttpPost]
        public IActionResult RemoveSize(long Id, long sizeId)
        {

            CategorySize catSize = catRepo.GetSpecificCatSize(Id, sizeId);
            Category category = catRepo.GetCategoryById(Id);

            category.CategoriesSizes.Remove(catSize);
            catRepo.UpdateCategory(category);
            GetProductsOfSpecCategory(Id);
            return View("EditCategoryForm", category);
            
        }

        public IActionResult UpdateCatPrice(long Id, long sizeId, decimal price)
        {
            Category category = catRepo.GetCategoryById(Id);
            //Model Check of Price.
            if (price <= 0)
            {
                ModelState.AddModelError(string.Empty, "Please enter Positive Price");
            }
            
            if(ModelState.IsValid)
            {
                CategorySize catSize = catRepo.GetSpecificCatSize(Id,sizeId);
                catSize.Price = price;
                catRepo.UpdateCategory(category);
            }
            GetProductsOfSpecCategory(Id);
            return View("EditCategoryForm", category);
        }

        [HttpPost]
        public IActionResult UpdateCatName(string name, long Id)
        {
            Category category = catRepo.GetCategoryById(Id);
            //Model State.
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError(nameof(category.Name), "Please enter category name");
                
            }
            if(ModelState.IsValid)
            {
                category.Name = name;
                catRepo.UpdateCategory(category);
            }
            GetProductsOfSpecCategory(Id);
            return View("EditCategoryForm", category);
        }

        [HttpGet]
        public IActionResult SizeForm(long sizeId=0)
        {
         
            Size size;

            if (sizeId == 0)
            {
              size= new Size();
              
            }
            else
            {
                size = sizeRepo.GetSize(sizeId);
                if (size == null)
                {
                    size = new Size();
                }
            }

            return View(size);
        }
        
        [HttpPost]
        public IActionResult AddOrEditSize(Size size)
        {

            SizeValidation(size);
            
            if (ModelState.IsValid)
            {
                sizeRepo.AddOrUpdateSize(size);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                
                return View(nameof(SizeForm),size);
            }
        }

        public void SizeValidation(Size size)
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

        [HttpGet]
        public IActionResult ProductForm (string productName,long prodId=0)
        {
            ViewBag.Product = productName;

            ViewBag.Categories = catRepo.GetSpecificProductCategories(productName);
            
            Product product = pizzaRepo.GetProduct(prodId);
            if (product==null)
            {
                 product = new Product();
            }
         
            return View(product);
            
        }

        [HttpPost]
        public IActionResult DeleteProduct(long prodId)
        {
            Product product = context.Products.FirstOrDefault(p => p.Id == prodId);
            pizzaRepo.DeleteProduct(product);
            return RedirectToAction("AllPizzas", "Home");
        }
        
        [HttpPost]
        public IActionResult AddOrUpdatePizza (Pizza pizza)
        {
           
            ViewBag.Product = pizza.GetType().Name;

            Category category=ProductValidation(pizza);
          
            if (ModelState.IsValid)
            {
                pizza.ImagePath = "img/Pizzas/" + pizza.ImagePath;
                pizza.Category = category;
                //Add new Pizza or Updating existing one.
                pizzaRepo.EditPizza(pizza);
                return RedirectToAction("AllPizzas", "Home");
            }
           
            else
            {
                return NotValid(pizza);
            }
        }


        public IActionResult AddOrUpdateDrink (Drink drink)
        {

            ViewBag.Product = drink.GetType().Name;

            Category c = ProductValidation(drink);

            if (ModelState.IsValid)
            {
                drink.ImagePath = "img/Drinks/" + drink.ImagePath;
                drink.Category = c;
                drinkRepo.EditDrink(drink);

                return RedirectToAction("GetDrinksBySize", "Home", 
                                    new { sizeName = drink.Category.Sizes.First().TheSize });
            }

            else
            {
                return NotValid(drink);
               
            }
        }


        public IActionResult AddOrUpdateTopping(Topping topping)
        {

            ViewBag.Product = topping.GetType().Name;

            Category category = ProductValidation(topping);

            if (ModelState.IsValid)
            {
                topping.ImagePath = "img/Toppings/" + topping.ImagePath;
                topping.Category = category;
                toppRepo.EditTopping(topping);

                return RedirectToAction("AllPizzas", "Home");                    
            }

            else
            {
                return NotValid(topping);
            }
        }


        public IActionResult CategoryForm()
        {
            Category category = new Category();
            ViewBag.ExistedTypes=catRepo.GetAllExistedTypes();

            return View(category);            
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            //ModelState for Category.
            CategoryValidation(category);

            if (ModelState.IsValid)
            {
                catRepo.AddCategory(category);
                TempData["catId"] = category.Id.ToString();

                return RedirectToAction(nameof(SizePriceForm));
            }
            else
            {
               
                ViewBag.ExistedTypes = catRepo.GetAllExistedTypes();
                return View("CategoryForm");
            }
            
        }
        
        public void CategoryValidation(Category category)
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


        public IActionResult SizePriceForm ()
        {
            CategorySize catSize = new CategorySize();
            ViewBag.ExistedSizes = sizeRepo.GetAllExistedSizes();
            ViewBag.Type = TempData["Type"];
            return View(catSize);
        }

        [HttpPost]
        public IActionResult AddSizePriceToCategory(CategorySize newCatSize)
        {
            SizePriceValidation(newCatSize);

            if (ModelState.IsValid)
            {
                Size size = sizeRepo.GetSizeBySizeName(newCatSize.Size.TheSize);
                newCatSize.Size = size;

                //To this (new Category) admin will add Size And Price for this Size.
                long categoryId = Convert.ToInt64(TempData["catId"].ToString());
                Category newCategory = catRepo.GetCategoryById(categoryId);

                
                TempData["Type"] = newCategory.Type;

                //If Admin Trying to add same size more than one time,
                //in this case the last choosen Price will be added to specific size.
                if (newCategory.Sizes.Contains(newCatSize.Size))
                {

                    CategorySize catS = newCategory.CategoriesSizes.
                        Where(c => c.Size.Id == newCatSize.Size.Id).FirstOrDefault();

                    //Edit price for specific size.
                    catS.Price = newCatSize.Price;

                }

                else
                {
                    newCategory.CategoriesSizes.Add(newCatSize);
                }

                context.SaveChanges();
                return RedirectToAction(nameof(SizePriceForm));
            }

            //If Model State Not Valid.
            else
            {
                ViewBag.ExistedSizes = sizeRepo.GetAllExistedSizes();
                return View(nameof(SizePriceForm));
            }

        }

        public IActionResult AddSizeToCategory(long id,string sizeName,decimal sizePrice)
        {
            Category category = catRepo.GetCategoryById(id);

            //Validation
            if (string.IsNullOrEmpty(sizeName))
            {
                ModelState.AddModelError(string.Empty, "Please choose size to add");
            }
            if (sizePrice <= 0)
            {
                ModelState.AddModelError(string.Empty, "Please enter positive price for size");
            }
            if (ModelState.IsValid)
            {
                Size size = sizeRepo.GetSizeBySizeName(sizeName);
                CategorySize catSize = new CategorySize { Size = size, Price = sizePrice };
                category.CategoriesSizes.Add(catSize);
                catRepo.UpdateCategory(category);

            }
            GetProductsOfSpecCategory(id);
            return View("EditCategoryForm", category);
        }

        public void SizePriceValidation(CategorySize categorySize)
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

        public Category ProductValidation(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError(nameof(product.Name), "Please Enter Product name");

            }
            
            Category c = catRepo.GetCategoryById(product.CategoryId);

            if (c == null)
            {
                ModelState.AddModelError(nameof(product.Category), "Please choose Product Category");
            }

            if (string.IsNullOrEmpty(product.Description))
            {
                ModelState.AddModelError(nameof(product.Description), "Please enter Product Discription");
            }

            return c;
        }

        public IActionResult NotValid(Product product)
        {
            ViewBag.Categories = catRepo.GetSpecificProductCategories(ViewBag.Product);
            Product p = context.Products.FirstOrDefault(p=>p.Id==product.Id);
            if (p != null)
            {
                 p = context.Products.FirstOrDefault(p => p.Id == product.Id);
            }
            else
            {
                p = new Product();
            }
            return View("ProductForm",p);
        }
        

        public void GetProductsOfSpecCategory(long catId)
        {
            IEnumerable<Product> products = catRepo.GetProductsOfSpecificCategory(catId);

            if (products.Count() != 0)
            {
                ViewBag.Products = products;
            }
            else
            {
                ViewBag.Products = null;
            }
        }

    }
}
