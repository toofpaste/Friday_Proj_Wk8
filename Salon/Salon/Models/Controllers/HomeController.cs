using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Salon.Controllers;
using Salon.Models;

namespace Salon.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
