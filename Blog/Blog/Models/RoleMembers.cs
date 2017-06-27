using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class RoleMembers
    {
        public IEnumerable<AppUser> Memebrs { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
        public IdentityRole Role { get; set; }
    }
}
