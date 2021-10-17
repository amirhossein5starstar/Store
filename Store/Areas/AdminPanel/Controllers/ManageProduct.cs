using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Core.DTOs.User;
using Store.Core.Services.Interfaces;
using Store.Data.Entities.Product;

namespace Store.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Route("{area}/{Controller}/{Action}/{id?}")]
    public class ManageProduct : Controller
    {
        public IAdminService _AdminService;

        public ManageProduct(IAdminService adminService)
        {
            _AdminService = adminService;
        }
        public async Task<IActionResult> ProductGroup()
        {

            ViewData["productGroupList"] = await _AdminService.productGroupList();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ProductGroup(AdminCreateProductGroup productGroup)
        {
            if (!ModelState.IsValid)
            {
                return View(productGroup);
            }
            await _AdminService.AddProductGroup(productGroup);

            return Redirect("/AdminPanel/ManageProduct/productGroup/");
        }




        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (await _AdminService.DeleteProductGroup(id))
            {
                return Redirect("/AdminPanel/ManageProduct/productGroup/");
            }

            return NotFound();
        }


    }
}
