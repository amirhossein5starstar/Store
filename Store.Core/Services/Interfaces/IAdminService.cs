using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.DTOs.User;
using Store.Data.Entities.User;

namespace Store.Core.Services.Interfaces
{
    public interface IAdminService
    {
        Task<int> CountUsers();
        Task<List<User>> PersonList(int take, int skip, string? search);

    }
}
