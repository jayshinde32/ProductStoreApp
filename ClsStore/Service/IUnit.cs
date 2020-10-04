using ClsStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Service
{
  public  interface IUnit
    {
        IQueryable<Unit> GetUnit();       
        Unit GetUnit(int id);
        void InsertUnit(Unit unit);
        void UpdateUnit(Unit unit);
        void DeleteUnit(Unit unit);
    }
}
