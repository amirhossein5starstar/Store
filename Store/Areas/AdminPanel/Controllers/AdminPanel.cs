using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AdminPanel : Controller
    {
      
        [Route("{controller}/Home")]
        public async Task<IActionResult> Home()
        {
            return View();
        }
    }
}
