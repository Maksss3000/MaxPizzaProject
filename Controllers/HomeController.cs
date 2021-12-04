using System;
using System.Collections.Generic;
using System.Linq;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Controllers
{
    public class HomeController : Controller
    {
        private PizzeriaDbContext context;
        private IPizzaRepository pizzaRepo;
        private IDrinkRepository drinkRepo;
        private ISnackRepository snackRepo;
        
        public Cart Cart { get; set; }
        public HomeController(IPizzaRepository pizzaRep, PizzeriaDbContext ctx,
                              Cart cartService,IDrinkRepository drinkRep,ISnackRepository snackRep)
                              
        {
            //Dependency Injection.
            drinkRepo = drinkRep;
            pizzaRepo = pizzaRep;
            snackRepo = snackRep;
            context = ctx;

            Cart = cartService;
        }
        public IActionResult Index()
        {
            ViewBag.Main = "Main";
            return RedirectToAction(nameof(AllPizzas));
            
        }

        public IActionResult SpecificProduct(long productId)
        {

            if (context.Products.FirstOrDefault(p => p.Id == productId) == null)
            {
                return RedirectToAction(nameof(AllPizzas));
            }
            return View(productId);  
            
        }

        [HttpGet]
        public IActionResult AllSnacks(string CatName)
        {

            ViewBag.SelectedCatName = CatName;
            
            if (snackRepo.GetSnacksByCategoryName(CatName).Any() != true)
            {
                ViewBag.SelectedCatName = "";
              
                return View(snackRepo.Snacks);
            }

            return View(snackRepo.GetSnacksByCategoryName(CatName));

        }

        [HttpGet]
        public IActionResult GetDrinksBySize(string sizeName)
        {

            long sizeId = drinkRepo.GetSizeIdBySizeName(sizeName);
            
            if (sizeId != 0)
            {
                ViewBag.Size = sizeName;
                return View(drinkRepo.GetDrinksBySize(sizeId));
            }
            //If there is no size with choosen size name.
            //Redirecting to main page.
            else
            {
                return RedirectToAction(nameof(AllPizzas));
            }
           
         
            }

        public IActionResult AllPizzas(string CatName)
        {
            ViewBag.SelectedCatName = CatName;
            if (pizzaRepo.GetPizzasByCategoryName(CatName).Any()!=true)
            {
                ViewBag.SelectedCatName = "";
                return View(pizzaRepo.Pizzas);
            }
           
            return View(pizzaRepo.GetPizzasByCategoryName(CatName));
           
        }

        [Authorize(Roles ="User,Admin")]
        public IActionResult SeeCart()
        {
            return View("Cart",Cart);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult AddToCart(OrderInformation order)
        {
                Cart.AddItem(order);
                return RedirectToAction(nameof(SeeCart));   
        }

        [HttpPost]
        public IActionResult RemoveFromCart(Guid orderId)
        {
            Cart.RemoveLine(orderId);
            return RedirectToAction(nameof(SeeCart));
        }

    }
}
