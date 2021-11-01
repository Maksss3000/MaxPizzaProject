using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaxPizzaProject.Components
{
    public class DropdownDrinks  : ViewComponent
    {
        private IDrinkRepository drinkRepo;
        public DropdownDrinks(IDrinkRepository repo)
        {
            drinkRepo = repo;
        }
        public IViewComponentResult Invoke()
        {
            return View(drinkRepo.Sizes);
        }
    }
}
