using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaxPizzaProject.Components
{
    public class TypeCategories : ViewComponent
    {
        
        private PizzeriaDbContext context;
        public TypeCategories (PizzeriaDbContext ctx)
        {
           
            context = ctx;
        }
        public IViewComponentResult Invoke(string type)
        {
            ViewBag.Type = type;
            return View(context.Categories.Where(c => c.Type == type));
        }
    }
}



