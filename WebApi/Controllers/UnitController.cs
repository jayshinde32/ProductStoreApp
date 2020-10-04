using ClsStore.Entity;
using ClsStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UnitController : ApiController
    {
        // GET: api/Unit
        private IUnit _Unit;      

        public UnitController(IUnit unit)
        {
            this._Unit = unit;            
        }
        public IHttpActionResult GetAllUnit()
        {
            IEnumerable<UnitModel> unit = _Unit.GetUnit()
               .Select(u => new UnitModel
               {
                   UnitID = u.UnitID,
                   UnitType=u.UnitType,                  
               }).ToList();

            if (unit.Count() == 0)
            {
                return NotFound();
            }
            return Ok(unit);           
        }

        // GET: api/Unit/5
        public IHttpActionResult Get(int id)
        {
            UnitModel model = new UnitModel();
            if(id!=0)
            {
                Unit unEntity = _Unit.GetUnit(id);
                model.UnitID = unEntity.UnitID;
                model.UnitType = unEntity.UnitType;
            }
            if (model.UnitID == 0)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // POST: api/Unit
        public IHttpActionResult Post(UnitModel model)
        {
            if (ModelState.IsValid)
            {
                Unit unit = new Unit
                {
                    UnitType = model.UnitType,
                    CreatedDate = DateTime.Now.Date,
                    ModifiedDate = DateTime.Now.Date

                };
                _Unit.InsertUnit(unit);
                return Ok();
            }
            else
                return BadRequest("Invalid data.");

           
        }

        // PUT: api/Unit/5
        public IHttpActionResult Put(UnitModel model)
        {
            if (ModelState.IsValid)
            {
                Unit unit = _Unit.GetUnit(model.UnitID);
                unit.UnitType = model.UnitType;
                unit.CreatedDate = DateTime.Now.Date;
                unit.ModifiedDate = DateTime.Now.Date;
               
                _Unit.UpdateUnit(unit);
                return Ok();
            }
            else
                return BadRequest("Invalid data.");
        }

        // DELETE: api/Unit/5
        public IHttpActionResult Delete(int id)
        {
            if(id!=0)
            {
                Unit unit = _Unit.GetUnit(id);
                _Unit.DeleteUnit(unit);

                return Ok();
            }
            else
                return BadRequest("Invalid data.");
        }
    }
}
