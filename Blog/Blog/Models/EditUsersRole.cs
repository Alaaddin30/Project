using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class EditUsersRole
    {
        public IEnumerable<AppUser> Users { get; set; }
        public IdentityRole Role { get; set; }
    }
}
