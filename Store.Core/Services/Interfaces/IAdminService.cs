using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.DTOs.User;
using Store.Data.Entities.Product;
using Store.Data.Entities.User;

namespace Store.Core.Services.Interfaces
{
    public interface IAdminService
    {
        Task<int> CountUsers();
        Task<int> CountProducts(int id);
        Task<List<User>> PersonList(int take, int skip, string? search);
        Task<List<ProductGroup>> productGroupList();
        Task<bool> DeleteProductGroup(int id);
        Task<bool> AddProductGroup(AdminCreateProductGroup adminCreateProductGroup);
        Task<List<Product>> ProductList(int id, int take, int skip, string? search);
        Task<int> FastCreateProduct(string ProductNameF, string ProductNameE, string price, int groupid);
        Task<bool> IsExistProduct(int id);
        Task<AdminEditProduct> GetProductById(int id);
        Task<bool> UpdateProductImageTitle(string ImageTitle, int ProductId);
        Task<string> GetProductImageTitleByProductId(int ProductId);
        Task<bool> SaveProductNamePrice(string ProductEnglishName, string ProductPersianName, string ProductPrice, int ProductId, bool IsShow);
        Task<ProductShowCard> GetProductForShowComponet(int id);


    }
}
