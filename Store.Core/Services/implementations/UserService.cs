using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.Convertors;
using Store.Core.Generator;
using Store.Core.Services.Interfaces;
using Store.Data.Context;
using Store.Data.Entities.User;

namespace Store.Core.Services.implementations
{
    public class UserService : IUserService
    {
        private  StoreContext _context;

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

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _context.SaveChanges();
            return true;
        }

        public async Task<int> GetUserIdByUserName(string userName)
        {
            return (await _context.Users.SingleAsync(s => s.UserName == userName)).UserId;
        }
    }
}
