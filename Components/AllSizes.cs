using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaxPizzaProject.Components
{
    public class AllSizes :  ViewComponent
    {

        private ISizeRepository sizeRepo;

        public AllSizes(ISizeRepository sizeRep)
        {
            sizeRepo = sizeRep;
        }

        public IViewComponentResult Invoke()
        {
            return View(sizeRepo.GetAllExistedSizes());
        }

    }
}
