using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class NewCategoryViewModel
    {
        [Required(ErrorMessage ="Enter Category Name")]
        [MaxLength(15,ErrorMessage ="15 letters Max Allowed")]
        public string Name { get; set; }
    }
}
