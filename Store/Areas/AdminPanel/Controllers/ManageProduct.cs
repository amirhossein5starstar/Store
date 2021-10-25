using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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


        public async Task<IActionResult> ProductList(int id, string? ErrorForm, string? search, int? pageid = 1)
        {
            if (ErrorForm != null)
            {
                ViewBag.ErrorForm = true;
            }
            ViewBag.FieldIsNull = false;
            ViewBag.ProductGroup = id;
            ViewBag.ProductPageID = pageid;
            ViewBag.Productsearchtitle = search;
            List<Product> productList;
            int take = 5;
            int skip = (pageid.Value - 1) * take;
            decimal Count = await _AdminService.CountProducts(id);
            if (search != null)
            {
                productList = await _AdminService.ProductList(id, take, skip, search);
                Count = productList.Count - 1;
                ViewBag.ProductPageCount = Math.Ceiling(Count / 5) + 1;
                return View(productList);
            }
            productList = await _AdminService.ProductList(id, take, skip, search);
            ViewBag.ProductPageCount = Math.Ceiling(Count / 5);
            return View(productList);
        }


        public async Task<IActionResult> FastCreateProduct(string ProductNameF, string ProductNameE, string price, int groupid)
        {
            if (price == null || ProductNameE == null || ProductNameF == null)
            {

                ViewBag.ProductGroup = groupid;
                return Redirect($"/adminpanel/ManageProduct/ProductList/{groupid}?ErrorForm=true");
            }

            int productId = await _AdminService.FastCreateProduct(ProductNameF, ProductNameE, price, groupid);
            return Redirect($"/adminpanel/ManageProduct/EditProduct/{productId}");
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            if (!(await _AdminService.IsExistProduct(id)))
            {
                return NotFound();
            }
            return View(await _AdminService.GetProductById(id));
        }

        #region SaveProductAPI
        [HttpPost]
        public async Task<IActionResult> SaveImage(IFormFile ProductImage, int ProductId)
        {
            if (ProductImage == null || ProductId == null)
            {
                return StatusCode(400);
            }

            if (await _AdminService.IsExistProduct(ProductId) == false)
            {
                return StatusCode(400);
            }


            string ImageTitle = Guid.NewGuid().ToString() + Path.GetExtension(ProductImage.FileName);
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/");
            using (var fileStream =
                new FileStream(Path.Combine(uploads, ImageTitle), FileMode.Create))
            {
                await ProductImage.CopyToAsync(fileStream);
            }

            if (await _AdminService.UpdateProductImageTitle(ImageTitle, ProductId))
            {
                return StatusCode(200);
            }

            return StatusCode(400);

        }

        [HttpPost]
        public async Task<IActionResult> SaveNamesPrice(string ProductEnglishName, string ProductPersianName, string ProductPrice, int ProductId, bool IsShow)
        {
            if (ProductEnglishName == null || ProductPersianName == null || ProductPrice == null || ProductId==null || IsShow == null)
            {
                return StatusCode(400);
            }
            if (await _AdminService.IsExistProduct(ProductId) == false)
            {
                return StatusCode(400);
            }




            return StatusCode(400);
        }
        #endregion

    }
}
