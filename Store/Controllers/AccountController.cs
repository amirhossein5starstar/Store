using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Core.Convertors;
using Store.Core.DTOs.User;
using Store.Core.Generator;
using Store.Core.Security;
using Store.Core.Senders;
using Store.Core.Services.Interfaces;
using Store.Data.Entities.User;

namespace Store.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #region Register

        [Route("Register")]
        public async Task<IActionResult> Register()
        {
            ViewBag.account = false;
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            ViewBag.account = false;
            if (!ModelState.IsValid)
            {
                return View(register);
            }


            if (await _userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }

            if (await _userService.IsExistEmail(register.Email))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }

            User user = new User()
            {
                ActiveCode = NameGenerator.GenerateUniqCode(),
                UserName = FixText.FixTxt(register.UserName),
                Email = FixText.FixTxt(register.Email),
                IsActive = false,
                Password = PasswordHash.EncodePasswordMd5(register.Password),
                UserAvatar = "Default.jpg"

            };


           await _userService.AddUser(user);



            string HostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            string ActiveUrl = HostUrl + "/" + @"ActiveAccount/?id=" + user.ActiveCode;
            string body = ($@"<a href='" + ActiveUrl + "'>برای فعال سازی اکانت خود کلیک کنید</a>");


            SendEmail.Send(user.Email, "فعالسازی HandBook", body);


            ViewBag.account = true;

            //return View("SuccessRegister", user);
            return View();
        }

        #endregion


    }
}
