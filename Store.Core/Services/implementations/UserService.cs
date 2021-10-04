using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Store.Core.Convertors;
using Store.Core.DTOs.User;
using Store.Core.Generator;
using Store.Core.Security;
using Store.Core.Services.Interfaces;
using Store.Data.Context;
using Store.Data.Entities.Product;
using Store.Data.Entities.User;

namespace Store.Core.Services.implementations
{
    public class UserService : IUserService
    {
        private StoreContext _context;

        public UserService(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> IsExistUserName(string userName)
        {
            return await _context.Users.AnyAsync(a => a.UserName == FixText.FixTxt(userName));
        }

        public async Task<bool> IsExistEmail(string email)
        {
            return await _context.Users.AnyAsync(a => a.Email == email);
        }

        public async Task<int> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(s => s.Email == FixText.FixTxt(email));
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _context.Users.SingleOrDefaultAsync(s => s.ActiveCode == activeCode);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(s => s.UserName == FixText.FixTxt(username));
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<bool> ActiveAccount(string activeCode)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetUserIdByUserName(string userName)
        {
            return (await _context.Users.SingleAsync(s => s.UserName == userName)).UserId;
        }

        public async Task<bool> LoginUser(User user, bool remember, HttpContext httpContext)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = remember
            };

            // impossible SignInAsync Without this line
           await httpContext.SignInAsync(principal, properties);

            return true;
        }

        public async Task<User?> GetUserByLoginViewModel(LoginViewModel user)
        {
            string hashPassword = PasswordHash.EncodePasswordMd5(user.Password);
            string email = FixText.FixTxt(user.Email);
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == hashPassword);
        }

        public async Task<UserProfileCardViewModel> GetUserInformationByUserName(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(s => s.UserName == username);
           UserProfileCardViewModel cardViewModel = new UserProfileCardViewModel()
           {
               Email = user.Email,
               PictureName = user.UserAvatar,
               UserName = user.UserName
           };
           return cardViewModel;
        }

        
    }
}
