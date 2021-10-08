using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.IsAdmin = User.Claims.Any(a => a.Type == ClaimTypes.Role);
            return View();
        }
    }
}
