using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Controllers
{
    public class HomeController : Controller
    {
        private IToppingRepository toppingRepo;
        private IPizzaRepository pizzaRepo;
        private ISizeRepository sizeRepo;
        public HomeController(IToppingRepository topRep,
                              IPizzaRepository pizzaRep,
                              ISizeRepository sizeRep)
        {
          
            toppingRepo = topRep;
            pizzaRepo = pizzaRep;
            sizeRepo = sizeRep;
        }
        public IActionResult Index()
        {
            ViewBag.Main = "Main";
            return View();
        }

        public IActionResult SpecificPizza(long pizzaId)
        {

            return View(pizzaId);
           
        }
        public IActionResult AllPizzas()
        {   
            return View(pizzaRepo.Pizzas);
        }
    }
}
