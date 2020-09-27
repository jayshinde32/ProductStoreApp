using ClsStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Service
{
  public interface ICategory
    {
        IQueryable<Category> GetCategories();
        Category GetCategory(int id);
        void InsertCat(Category category);
        void UpdateCat(Category category);
        void DeleteCat(Category category);
    }
}
