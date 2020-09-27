using ClsStore.Entity;
using ClsStore.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Operation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDBContext _context;
        private IDbSet<T> _entities;
        private IEnumerable<Product> _product;
        public Repository(IDBContext context)
        {
            this._context = context;
        }
        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {                   
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }
      
        public T GetById(object id)
        {            
            return this.Entities.Find(id);
        }       
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public IEnumerable<T> GetProductsBySP()
        {
            _product = _context.GetProductBySP();
            return (IEnumerable<T>)_product;
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException Ex)
            {
                var msg = string.Empty;

                foreach (var validationErrors in Ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, Ex);
                throw fail;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException Ex)
            {
                var msg = string.Empty;
                foreach (var validationErrors in Ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, Ex);
                throw fail;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Remove(entity);
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException Ex)
            {
                var msg = string.Empty;

                foreach (var validationErrors in Ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, Ex);
                throw fail;
            }
        }

       
    }
}
