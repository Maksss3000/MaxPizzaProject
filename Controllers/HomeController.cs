using System;
using System.Linq;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Controllers
{
    public class HomeController : Controller
    {
        private PizzeriaDbContext context;
        private IPizzaRepository pizzaRepo;
        private IDrinkRepository drinkRepo;
        
        public Cart Cart { get; set; }
        public HomeController(IPizzaRepository pizzaRep, PizzeriaDbContext ctx,
                              Cart cartService,IDrinkRepository drinkRep)
                              
        {
            //Dependency Injection.
            drinkRepo = drinkRep;
            pizzaRepo = pizzaRep;
            context = ctx;

            Cart = cartService;
        }
        public IActionResult Index()
        {
            ViewBag.Main = "Main";
            return RedirectToAction(nameof(AllPizzas));
        }

        public IActionResult SpecificPizza(long pizzaId)
        {
            if (pizzaRepo.GetPizzaById(pizzaId) == null)
            {
                return RedirectToAction(nameof(AllPizzas));
            }

            return View(pizzaId);  
        }

        [HttpGet]
        public IActionResult GetDrinksBySize(long sizeId,string sizeName)
        {
            
            ViewBag.Size = sizeName;

            return View(drinkRepo.GetDrinksBySize(sizeId));
         
            }

        public IActionResult AllPizzas(string pizzaCatName)
        {
            ViewBag.SelectedCatName = pizzaCatName;
            if (pizzaRepo.GetPizzasByCategoryName(pizzaCatName).Any()!=true)
            {
                ViewBag.SelectedCatName = "";
                return View(pizzaRepo.Pizzas);
            }
           
            return View(pizzaRepo.GetPizzasByCategoryName(pizzaCatName));
           
        }

        public IActionResult SeeCart()
        {
            return View("Cart",Cart);
        }

        [HttpPost]
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
