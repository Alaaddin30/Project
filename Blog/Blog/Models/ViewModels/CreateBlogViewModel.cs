using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class CreateBlogViewModel
    {
        [Required(ErrorMessage ="Choose Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Enter Title")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Enter Body")]
        [MinLength(200,ErrorMessage ="Type 200 characters at least")]
        public string Body { get; set; }
    }
}
