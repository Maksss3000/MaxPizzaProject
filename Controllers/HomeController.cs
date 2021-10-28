using System;
using System.Collections.Generic;
using System.Linq;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;


namespace MaxPizzaProject.Controllers
{
    public class HomeController : Controller
    {
        private PizzeriaDbContext context;
        private IPizzaRepository pizzaRepo;
        
        public Cart Cart { get; set; }
        public HomeController(IPizzaRepository pizzaRep, PizzeriaDbContext ctx,Cart cartService)
                              
        {
            pizzaRepo = pizzaRep;
            context = ctx;

            //Dependency Injection.
            Cart = cartService;
        }
        public IActionResult Index()
        {
            ViewBag.Main = "Main";
            return View();
        }

        public IActionResult SpecificPizza(long pizzaId)
        {
            if (pizzaRepo.GetPizzaById(pizzaId) == null)
            {
                return RedirectToAction(nameof(AllPizzas));
            }

            return View(pizzaId);  
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
