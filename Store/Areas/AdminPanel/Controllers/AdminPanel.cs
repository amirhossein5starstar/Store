using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Store.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Route("{area}/{Action}/{id?}")]
    public class AdminPanel : Controller
    {

        [Authorize(Policy = "AdminPanel")]
        [Authorize(Policy = "ManageUser")]
        public async Task<IActionResult> Home()
        {
            return View();
        }
    }
}
