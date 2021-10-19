
using System.Collections.Generic;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaxPizzaProject.Components
{
    public class Toppings : ViewComponent
    {
        private IToppingRepository topRepository;

        public Toppings (IToppingRepository topRepo)
        {
            topRepository = topRepo;
        }

        public IViewComponentResult Invoke()
        {
            
            IEnumerable<long> ids = topRepository.GetToppingCategoriesIds();
            
            ViewBag.ToppingPrices = topRepository.GetToppingPrice(ids, ViewBag.SizeId);

            return View(topRepository.GetToppCategories());
            
        }
    }
}
