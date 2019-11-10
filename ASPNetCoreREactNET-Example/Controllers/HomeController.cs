using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreREactNET_Example.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreREactNET_Example.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            //Test data
            var user = new UserModel { Name = "Test User", Age = 18 };
            return View(user);
        }
    }
}
