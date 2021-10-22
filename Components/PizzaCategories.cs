using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaxPizzaProject.Components
{
    public class PizzaCategories : ViewComponent
    {
        private IPizzaRepository pizzaRepo;
        public PizzaCategories(IPizzaRepository repo)
        {
            pizzaRepo = repo;
        }
        public IViewComponentResult Invoke()
        {
            return View(pizzaRepo.GetAllPizzasCategories());
        }
    }
}
