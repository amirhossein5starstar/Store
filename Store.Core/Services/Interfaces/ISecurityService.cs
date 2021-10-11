using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.DTOs.User;
using Store.Data.Entities.User;

namespace Store.Core.Services.Interfaces
{
    public interface ISecurityService
    {
        Task<List<string>> GetRoleByUserId(int id);
        Task<List<Role>> GetAllRoles();
        Task<List<Role>> GetUserRoles(int id);
        Task<bool> AddRoleToUser(List<AdminPersonRolesViewModel> adminPersonRolesViewModel, int id);

    }
}
