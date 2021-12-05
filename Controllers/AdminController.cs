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
        private ISnackRepository snackRepo;

        private ValidationClass validation;
        //Dependency Injection.
        public AdminController(PizzeriaDbContext ctx, IDrinkRepository drinkRep,
                               IPizzaRepository pizzaRep, ICategoryRepository catRep, 
                               IToppingRepository topRep, ISizeRepository sizRep,
                               ValidationClass validat,
                               ISnackRepository snackRep)
        {
            context = ctx;
            pizzaRepo = pizzaRep;
            catRepo = catRep;
            drinkRepo = drinkRep;
            toppRepo = topRep;
            sizeRepo = sizRep;
            snackRepo = snackRep;

            validation = validat;
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
            validation.CategoryDeletingValidation(ModelState, cat);

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
            //If there is no such Category,redirect to Main Page.
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
            validation.PriceValidation(ModelState,price);
            
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
            validation.CategoryNameValidation(ModelState, category, name);
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
            size = sizeRepo.GetSize(sizeId);
            if (sizeId == 0|| size==null)
            {
              size= new Size();
              
            }

            return View(size);
        }
        
        [HttpPost]
        public IActionResult AddOrEditSize(Size size)
        {

            validation.SizeNameValidation(ModelState, size);
            
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

            Category category = validation.ProductValidation(ModelState, pizza);
          
            if (ModelState.IsValid)
            {
               // pizza.ImagePath = pizza.ImagePath; 
                    //"img/Pizzas/" + pizza.ImagePath;
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

            Category category = validation.ProductValidation(ModelState, drink);

            if (ModelState.IsValid)
            {
               // drink.ImagePath = "img/Drinks/" + drink.ImagePath;
                drink.Category = category;
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

            Category category = validation.ProductValidation(ModelState, topping);

            if (ModelState.IsValid)
            {
                //topping.ImagePath = "img/Toppings/" + topping.ImagePath;
                topping.Category = category;
                toppRepo.EditTopping(topping);

                return RedirectToAction("AllPizzas", "Home");                    
            }

            else
            {
                return NotValid(topping);
            }
        }


        public IActionResult AddOrUpdateSnack(Snack snackProduct)
        {
            ViewBag.Product = snackProduct.GetType().Name;

            Category category = validation.ProductValidation(ModelState, snackProduct);

            if (ModelState.IsValid)
            {
                //snackProduct.ImagePath = "img/Snacks/" + snackProduct.ImagePath;
                snackProduct.Category = category;
                //Add or Update Snack.
                snackRepo.EditSnack(snackProduct);
               
                return RedirectToAction("AllPizzas", "Home");
            }

            else
            {
                return NotValid(snackProduct);
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
            validation.CategoryValidation(ModelState, category);
          
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
            
            validation.SizePriceValidation(ModelState, newCatSize);

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
            validation.AddNewSizeValidation(ModelState, sizeName, sizePrice);

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

        public IActionResult NotValid(Product product)
        {
            ViewBag.Categories = catRepo.GetSpecificProductCategories(ViewBag.Product);
            Product prod = context.Products.FirstOrDefault(p=>p.Id==product.Id);
            if (prod != null)
            {
                 prod = context.Products.FirstOrDefault(p => p.Id == product.Id);
            }
            else
            {
                prod = new Product();
            }
            return View("ProductForm",prod);
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
