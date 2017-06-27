using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Enter User name")]
        [Display(Name ="User name")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Enter password")]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
