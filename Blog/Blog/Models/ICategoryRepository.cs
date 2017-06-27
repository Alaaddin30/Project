using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        void Add(Category _category);
        int Delete(int id);
        void Update(Category _category);
    }
}
