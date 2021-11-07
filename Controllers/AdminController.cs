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

        public AdminController(PizzeriaDbContext ctx, 
                               IPizzaRepository pizzaRep,ICategoryRepository catRep)
        {
            context = ctx;
            pizzaRepo = pizzaRep;
            catRepo = catRep;
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
                ViewBag.Categories = catRepo.GetSpecificProductCategories(ViewBag.Product);
                return View("ProductForm");
            }
          
        }

        public Category ProductValidation(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError(nameof(product.Name), "Enter Pizza name");

            }
            
            Category c = catRepo.GetCategoryById(product.CategoryId);

            if (c == null)
            {
                ModelState.AddModelError(nameof(product.Category), "Choose Category for Pizza");
            }

            if (string.IsNullOrEmpty(product.Description))
            {
                ModelState.AddModelError(nameof(product.Description), "Please enter Pizza Discription");
            }

            return c;
        }
        
    }
}
