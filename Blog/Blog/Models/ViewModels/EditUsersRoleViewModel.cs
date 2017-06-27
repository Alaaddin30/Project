using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class EditUsersRoleViewModel
    {
        public string[] Ids { get; set; }
        public string Name { get; set; }
        public string RoleId { get; set; }
    }
}
