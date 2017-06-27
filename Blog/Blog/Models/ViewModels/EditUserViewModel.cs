using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        //[Required] // not required
        [UIHint("password")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,})$", ErrorMessage = "Enter valid Email")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
