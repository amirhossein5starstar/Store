using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Services.Interfaces;

namespace Store.Areas.AdminPanel.ViewComponents.ManageProduct
{
    public class ProductShowCard : ViewComponent
    {
        private IAdminService _adminService;

        public ProductShowCard(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int ProductId)
        {

            return await Task.FromResult((IViewComponentResult)View("ProductShowCard", await _adminService.GetProductForShowComponet(ProductId)));
            
        }



    }
}
