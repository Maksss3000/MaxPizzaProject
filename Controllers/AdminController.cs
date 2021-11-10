using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace MaxPizzaProject.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AdminController : Controller
    {

        private PizzeriaDbContext context;
        private IPizzaRepository pizzaRepo;
        private ICategoryRepository catRepo;
        private IDrinkRepository drinkRepo;
        private IToppingRepository toppRepo;
        private ISizeRepository sizeRepo;

        public AdminController(PizzeriaDbContext ctx, IDrinkRepository drinkRep,
                               IPizzaRepository pizzaRep,ICategoryRepository catRep,IToppingRepository topRep, ISizeRepository sizRep)
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
        public IActionResult ChooseProductForAdding ()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProductForm (string productName)
        {
            ViewBag.Product = productName;

            ViewBag.Categories = catRepo.GetSpecificProductCategories(productName);

            Product p = new Product();
            return View(p);
            
        }

        public IActionResult AddPizza(Pizza p)
        {

            ViewBag.Product = p.GetType().Name;

            Category c=ProductValidation(p);
          
            if (ModelState.IsValid)
            {
                p.ImagePath = "img/Pizzas/" + p.ImagePath;
                p.Category = c;
                pizzaRepo.AddPizza(p);
                return RedirectToAction("AllPizzas", "Home");
            }
           
            else
            {
                return NotValid();
               // ViewBag.Categories = catRepo.GetSpecificProductCategories(ViewBag.Product);
               // return View("ProductForm");
            }
        }


        public IActionResult AddDrink (Drink drink)
        {

            ViewBag.Product = drink.GetType().Name;

            Category c = ProductValidation(drink);

            if (ModelState.IsValid)
            {
                drink.ImagePath = "img/Drinks/" + drink.ImagePath;
                drink.Category = c;
                drinkRepo.AddDrink(drink);
                return RedirectToAction("GetDrinksBySize", "Home", 
                                    new { sizeName = drink.Category.Sizes.First().TheSize });
            }

            else
            {
                return NotValid();
               
            }
        }


        public IActionResult AddTopping (Topping topping)
        {

            ViewBag.Product = topping.GetType().Name;

            Category c = ProductValidation(topping);

            if (ModelState.IsValid)
            {
                topping.ImagePath = "img/Toppings/" + topping.ImagePath;
                topping.Category = c;
                toppRepo.AddTopping(topping);

                return RedirectToAction("AllPizzas", "Home");
                                    
            }

            else
            {
                return NotValid();
            }
        }


        public IActionResult CategoryForm()
        {
            Category c = new Category();
            ViewBag.ExistedTypes=catRepo.GetAllExistedTypes();
            return View(c);            
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
                Category c = new Category();
                ViewBag.ExistedTypes = catRepo.GetAllExistedTypes();
                return View(nameof(CategoryForm),c);
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
                CategorySize catSize = new CategorySize();
                ViewBag.ExistedSizes = sizeRepo.GetAllExistedSizes();
                return View(nameof(SizePriceForm),catSize);
            }

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

        public IActionResult NotValid()
        {
            ViewBag.Categories = catRepo.GetSpecificProductCategories(ViewBag.Product);
            return View("ProductForm");
        }
        
    }
}
