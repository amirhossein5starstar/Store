﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.DTOs.User
{
    public class AdminPersonListViewModel
    {
        [Display(Name = "ID کاربر")]
        public int Id { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }

    public class AdminPersonRolesViewModel
    {
        [Display(Name = "RoleID")]
        public int RoleId { get; set; }
        [Display(Name = "دسترسی")]
        public string RoleTitle { get; set; }
        [Display(Name = "دسترسی دارد/ندارد")]
        public bool IsChecked { get; set; }

    }

    public class AdminCreateProductGroup
    {

        [Required]
        [MaxLength(50)]
        [Display(Name = "نام گروه")]
        public string Title { get; set; }

    }
}
