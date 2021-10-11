using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.DTOs.User;
using Store.Core.Services.Interfaces;
using Store.Data.Context;
using Store.Data.Entities.User;

namespace Store.Core.Services.implementations
{
    public class SecurityService : ISecurityService
    {
        private StoreContext _context;

        public SecurityService(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRoleToUser(List<AdminPersonRolesViewModel> adminPersonRolesViewModel, int id)
        {
            adminPersonRolesViewModel = adminPersonRolesViewModel.Where(w => w.IsChecked == true).ToList();
            //get all user role
          var alluserRoles= await _context.UserRoles
                .Where(r => r.UserId == id)
                .ToListAsync();
            //remove them
            alluserRoles.ForEach(r => _context.UserRoles.Remove(r));

            //add roles to user
            foreach (var item in adminPersonRolesViewModel)
            {
               await _context.UserRoles.AddAsync(new UserRole()
                {
                    RoleId = item.RoleId,
                    UserId = id
                });
            }



           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<List<string>> GetRoleByUserId(int id)
        {
            return await _context.UserRoles
                .Where(w => w.UserId == id)
                .Select(s => s.Role)
                .Select(s => s.RoleTitle)
                .ToListAsync();

        }

        public async Task<List<Role>> GetUserRoles(int id)
        {
            return await _context.UserRoles.Where(w => w.UserId == id).Select(s => s.Role).ToListAsync();
        }
    }
}
