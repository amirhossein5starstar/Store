using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.DTOs.User
{
    public class UserProfileCardViewModel
    {
        public string UserName { get; set; }
        public string PictureName { get; set; }
        public string Email { get; set; }
    }

   


}
