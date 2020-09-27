using ClsStore.Entity;
using ClsStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Operation
{
    public class CategoryOperation : ICategory
    {
        private IRepository<Category> _category;

        public CategoryOperation(IRepository<Category> category)
        {
            this._category = category;

        }
        public IQueryable<Category> GetCategories()
        {
            return _category.Table;
        }

        public Category GetCategory(int id)
        {
            return _category.GetById(id);
        }
        public void DeleteCat(Category category)
        {
             _category.Delete(category);
        }
       
        public void InsertCat(Category category)
        {
            _category.Insert(category);
        }

        public void UpdateCat(Category category)
        {
            _category.Update(category);
        }
    }
}
