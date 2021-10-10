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
    }
}
