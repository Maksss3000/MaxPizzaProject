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
            return View();
        }

        public IActionResult SpecificPizza(long id,long toppCatId,long sizeId=1)
        {
            //Вынести все в отдельный класс с полями?
            ViewBag.SizeId = sizeId;
            ViewBag.CatId = toppCatId;
            ViewBag.Size = sizeRepo.GetSize(sizeId);
     
            if (toppCatId != 0)
            {
                ViewBag.Toppings = toppingRepo.GetToppingsByCategory(toppCatId);
            }

            Pizza p = pizzaRepo.GetPizzaById(id);
         
            if (p != null)
            {
                ViewBag.Price = pizzaRepo.GetPizzaPrice(sizeId, p.CategoryId);
                return View(p);
            }

            return RedirectToAction("Index");
        }
        public IActionResult AllPizzas()
        {   
            return View(pizzaRepo.Pizzas);
        }
    }
}
