using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Store.Core.DTOs.User;
using Store.Data.Entities.User;

namespace Store.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsExistUserName(string userName);
        Task<bool> IsExistEmail(string email);
        Task<int> AddUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int userId);
        Task<User> GetUserByActiveCode(string activeCode);
        Task<User> GetUserByUserName(string username);
        void UpdateUser(User user);
        Task<bool> ActiveAccount(string activeCode);
        Task<int> GetUserIdByUserName(string userName);
        Task<bool> LoginUser(User user, bool remember, HttpContext httpContext);
        Task<User?> GetUserByLoginViewModel(LoginViewModel user);

    }
}
