using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaxPizzaProject.Components
{
    public class ExistedProducts : ViewComponent
    {
        private ICategoryRepository categoryRepo;

        public ExistedProducts(ICategoryRepository repo)
        {
            categoryRepo = repo;
        }

        public IViewComponentResult Invoke()
        {

            return View(categoryRepo.GetAllExistedTypes());

        }

    }
}
