using ClsStore.Entity;
using ClsStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Operation
{
   public class UnitOperation : IUnit
    {
        private IRepository<Unit> _unit;
        public UnitOperation(IRepository<Unit> unit)
        {
            this._unit = unit;
        }
        public IQueryable<Unit> GetUnit()
        {
            return _unit.Table;
        }
        public Unit GetUnit(int id)
        {
            return _unit.GetById(id);
        }

        public void InsertUnit(Unit unit)
        {
             _unit.Insert(unit);
        }

        public void UpdateUnit(Unit unit)
        {
            _unit.Update(unit);
        }

        public void DeleteUnit(Unit unit)
        {
            _unit.Delete(unit);
        }
    }
}
