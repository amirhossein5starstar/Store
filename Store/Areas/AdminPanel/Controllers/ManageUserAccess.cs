using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Core.DTOs.User;
using Store.Core.Services.Interfaces;
using Store.Data.Entities.User;

namespace Store.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Route("{area}/{Controller}/{Action}/{id?}")]
    public class ManageUserAccess : Controller
    {
        private IAdminService _AdminService;

        public ManageUserAccess(IAdminService adminService)
        {
            _AdminService = adminService;
        }


        public async Task<IActionResult> UserList(string? search, int? pageid=1 )
        {
           
            List<User> personList;
            int take = 5;
            int skip = (pageid.Value - 1) * take;
            decimal Count = await _AdminService.CountUsers();
            ViewBag.PageID = pageid;
            ViewBag.searchtitle = search;
            if (search != null)
            {
                personList = await _AdminService.PersonList(take, skip, search);
                Count = personList.Count;
                ViewBag.PageCount = Math.Ceiling(Count / 5)+1;
                return View(personList);
            }

            personList = await  _AdminService.PersonList(take, skip, search);


            ViewBag.PageCount = Math.Ceiling(Count / 5);
            
            return View(personList);
        }
    }
}
