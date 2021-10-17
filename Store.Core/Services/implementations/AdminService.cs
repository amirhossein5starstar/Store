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
    }
}
