using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Store.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        [Route("UserPanel")]
        public async Task<IActionResult> UserPanel()
        {
            return View();
        }
    }
}
