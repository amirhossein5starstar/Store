using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.Core.DTOs.User;
using Store.Core.Services.Interfaces;
using Store.Data.Context;
using Store.Data.Entities.Product;
using Store.Data.Entities.User;

namespace Store.Core.Services.implementations
{
    public class AdminService : IAdminService
    {
        private StoreContext _context;

        public AdminService(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> CountUsers()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<List<User>> PersonList(int take, int skip, string? search)
        {
            IQueryable<User> PersonList = _context.Users;
            if (search != null)
            {
                PersonList = PersonList
                    .Where(w => w.Email
                        .Contains(search) || w.UserName
                        .Contains(search) || w.UserId
                        .ToString().Contains(search));
            }

            PersonList = PersonList.OrderByDescending(p => p.UserId).Skip(skip).Take(take);


            return await PersonList.ToListAsync();

        }

        public async Task<List<ProductGroup>> productGroupList()
        {
            return await _context.ProductGroups.Where(w => w.IsDelete == false).ToListAsync();
        }

        public async Task<bool> DeleteProductGroup(int id)
        {


            var ProductGroup = await _context.ProductGroups.SingleOrDefaultAsync(s => s.Id == id);
            if (ProductGroup == null)
            {
                return false;
            }
            if (await _context.Products.AnyAsync(a => a.ProductGroupId == id))
            {
                ProductGroup.IsDelete = true;
                _context.ProductGroups.Update(ProductGroup);
                await _context.SaveChangesAsync();
                return true;
            }


            _context.Remove(ProductGroup);
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<bool> AddProductGroup(AdminCreateProductGroup adminCreateProductGroup)
        {
            var Group = new ProductGroup() { IsDelete = false, Title = adminCreateProductGroup.Title };
            await _context.AddAsync(Group);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CountProducts(int id)
        {
            return await _context.Products.Where(w => w.ProductGroupId == id).CountAsync();
        }

        public async Task<List<Product>> ProductList(int id, int take, int skip, string search)
        {
            IQueryable<Product> ProductList = _context.Products.Where(w => w.ProductGroupId == id);
            if (search != null)
            {
                ProductList = ProductList
                    .Where(w => w
                        .EnglishName
                        .Contains(search) || w.PersianName
                        .Contains(search) || w.Id
                        .ToString().Contains(search) || w.Price
                        .Contains(search));
            }

            ProductList = ProductList.OrderByDescending(p => p.Id).Skip(skip).Take(take);


            return await ProductList.ToListAsync();
        }

        public async Task<int> FastCreateProduct(string ProductNameF, string ProductNameE, string price, int groupid)
        {
            var product = _context.Products;
            Product NewProduct = new Product()
            {
                EnglishName = ProductNameE,
                PersianName = ProductNameF,
                Price = price,
                ProductGroupId = groupid,
                ImageTitle = "Default.jpg",
                IsShowInStore = false
            };
            await product.AddAsync(NewProduct);
            await _context.SaveChangesAsync();

            return NewProduct.Id;



        }

        public async Task<bool> IsExistProduct(int id)
        {
            return await _context.Products.AnyAsync(a => a.Id == id);
        }

        public async Task<AdminEditProduct> GetProductById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(s => s.Id == id);
            var productForView = new AdminEditProduct()
            {
                Id = product.Id,
                PersianName = product.PersianName,
                EnglishName = product.EnglishName,
                Price = product.Price,
                ImageTitle = product.ImageTitle,
                IsShowInStore = product.IsShowInStore,
                ProductReview = product.ProductReview
            };
            return productForView;
        }

        public async Task<bool> UpdateProductImageTitle(string ImageTitle, int ProductId)
        {
            var product = await _context.Products.SingleOrDefaultAsync(s => s.Id == ProductId);
            product.ImageTitle = ImageTitle;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
