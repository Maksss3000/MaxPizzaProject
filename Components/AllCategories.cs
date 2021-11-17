using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaxPizzaProject.Components
{
    public class AllCategories : ViewComponent
    {
        private ICategoryRepository catRepo;

        public AllCategories(ICategoryRepository catRep)
        {
            catRepo = catRep;
        }

        public IViewComponentResult Invoke()
        {
            
            return View(catRepo.GetAllExistedTypes());
        }
        
    }
}
