using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Services.Interfaces;

namespace Store.ViewComponents.UserPanel
{
    

    public class UserProfileCard:ViewComponent
    {
        private IUserService userService;

        public UserProfileCard(IUserService userService)
        {
            this.userService = userService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("UserProfileCard", await userService.GetUserInformationByUserName(User.Identity.Name)));
            
        }
    }
}
