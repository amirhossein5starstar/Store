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
        private IUserService _userService;
        private ISecurityService _securityService;

        public ManageUserAccess(IAdminService adminService, IUserService userService, ISecurityService securityService)
        {
            _AdminService = adminService;
            _userService = userService;
            _securityService = securityService;
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

        public async Task<IActionResult> UserAccess(int id,bool? change)
        {
            ViewData["change"] = change;
            ViewData["UserName"] = await _userService.GetUserNameById(id);
            List<Role> allRoles = await _securityService.GetAllRoles();
            ViewData["RoleCount"] = allRoles.Count;
            List<Role> userRoles = await _securityService.GetUserRoles(id);

            List<AdminPersonRolesViewModel> Roles = new List<AdminPersonRolesViewModel>();
            foreach (Role role in allRoles)
            {
                Roles.Add(new AdminPersonRolesViewModel()
                {
                    IsChecked = userRoles.Any(a=>a.RoleId==role.RoleId) ,
                    RoleId = role.RoleId,
                    RoleTitle = role.RoleTitle
                });
            }
            return View(Roles);
        }

        [HttpPost]
        public async Task<IActionResult> UserAccess(List<AdminPersonRolesViewModel> adminPersonRolesViewModel, int id)
        {
           await _securityService.AddRoleToUser(adminPersonRolesViewModel, id);
            return Redirect($"/AdminPanel/ManageUserAccess/UserAccess/{id}?change=true");
        }
    }
}
