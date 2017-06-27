using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="Enter User name")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Enter Password")]
        [Compare("ConfirmPassword",ErrorMessage ="Passwords don't match")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm Pasword")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Enter Email")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,})$", ErrorMessage ="Enter Email in Form example@example.com")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
