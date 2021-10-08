using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<string>> GetRoleByUserId(int id)
        {
            return await _context.UserRoles
                .Where(w => w.UserId == id)
                .Select(s => s.Role)
                .Select(s => s.RoleTitle)
                .ToListAsync();

        }
    }
}
