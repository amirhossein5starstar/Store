using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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


            SendEmail.Send(user.Email, "فعالسازی Store", body);


            ViewBag.account = true;

            //return View("SuccessRegister", user);
            return View();
        }

        #endregion

        #region Login

        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {

            if (ModelState.IsValid)
            {
                return View(login);
            }  
            var httpcontext = this.HttpContext;
            var user = await _userService.GetUserByLoginViewModel(login);

            //if http context of this page doesn't sent to service the service try to create a new one and it is null than we have null exception
            if (user != null)
            {
                await _userService.LoginUser(user, login.RememberMe, httpcontext);
                return Redirect("/");
            }

            ModelState.AddModelError("Email", errorMessage: "کاربری یافت نشد");
            return View(login);
        }

        #endregion

        #region ActiveAccount

        [Route("ActiveAccount")]
        public async Task<IActionResult> ActiveAccount(string id)
        {

            //this line save http context of this page in varible 
            var httpcontext = this.HttpContext;
            var user = await _userService.GetUserByActiveCode(id);

            //if http context of this page doesn't sent to service the service try to create a new one and it is null than we have null exception
            if (await _userService.ActiveAccount(id))
            {
                await _userService.LoginUser(user, true, httpcontext);
                return Redirect("/");
            }

            return NotFound();
        }

        #endregion

        #region Logout
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion

        #region ResetPassword

        [Route("UserChangePassword")]
        public async Task<IActionResult> UserChangePassword()
        {
            return View();
        }

        [Route("UserChangePassword")]
        [HttpPost]
        public async Task<IActionResult> UserChangePassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var user = await _userService.GetUserByUserName(User.Identity.Name);
            user.Password = PasswordHash.EncodePasswordMd5(resetPasswordViewModel.Password);
            _userService.UpdateUser(user);
            _userService.SaveNotAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _userService.LoginUser(user, true, this.HttpContext);
            return Redirect("UserPanel");
        }

        #endregion





    }
}
