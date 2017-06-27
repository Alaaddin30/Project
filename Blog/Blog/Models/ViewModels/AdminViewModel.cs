using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models.ViewModels
{
    public class AdminViewModel<T>
    {
        public IEnumerable<T> Sequuence { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
