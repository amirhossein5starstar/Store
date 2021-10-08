using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data.Entities.User;

namespace Store.Core.Services.Interfaces
{
    public interface ISecurityService
    {
        Task<List<string>> GetRoleByUserId(int id);

    }
}
