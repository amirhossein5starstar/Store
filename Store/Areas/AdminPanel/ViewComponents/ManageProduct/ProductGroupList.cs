using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Services.Interfaces;

namespace Store.Areas.AdminPanel.ViewComponents.ManageProduct
{
    public class ProductGroupList : ViewComponent
    {
        private IAdminService _adminService;

        public ProductGroupList(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("productGroupListCard", await _adminService.productGroupList()));
        }
    }
}
