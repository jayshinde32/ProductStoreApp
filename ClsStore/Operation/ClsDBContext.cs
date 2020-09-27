using ClsStore.Entity;
using ClsStore.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Operation.Models
{
    public class ClsDBContext:DbContext,IDBContext
    {
        public ClsDBContext() : base("name=DBConnection")
        {
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //  //  modelBuilder.Entity<Product>().MapToStoredProcedures();
        //}
       
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public IEnumerable<Product> GetProductBySP()
        {
            return base.Database.SqlQuery<Product>("GetProductSummary").ToList();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}