﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MaxPizzaProject.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult OrderOwnPizza()
        {
            ViewBag.Title = "OrderOwnPizza";
            return View();
        }
    }
}