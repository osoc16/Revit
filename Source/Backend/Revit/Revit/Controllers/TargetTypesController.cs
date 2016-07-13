using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Revit.Models;

namespace Revit.Controllers
{
    public class TargetTypesController : ApiController
    {
        private SelorBusiness_SOCEntities db = new SelorBusiness_SOCEntities();

        // GET: api/TargetTypes
        public IQueryable<TargetType> GetTargetType()
        {
            return db.TargetType;
        }

        // GET: api/TargetTypes/5
        [ResponseType(typeof(TargetType))]
        public IHttpActionResult GetTargetType(int id)
        {
            TargetType targetType = db.TargetType.Find(id);
            if (targetType == null)
            {
                return NotFound();
            }

            return Ok(targetType);
        }

        // PUT: api/TargetTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTargetType(int id, TargetType targetType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != targetType.Id)
            {
                return BadRequest();
            }

            db.Entry(targetType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TargetTypes
        [ResponseType(typeof(TargetType))]
        public IHttpActionResult PostTargetType(TargetType targetType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TargetType.Add(targetType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TargetTypeExists(targetType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = targetType.Id }, targetType);
        }

        // DELETE: api/TargetTypes/5
        [ResponseType(typeof(TargetType))]
        public IHttpActionResult DeleteTargetType(int id)
        {
            TargetType targetType = db.TargetType.Find(id);
            if (targetType == null)
            {
                return NotFound();
            }

            db.TargetType.Remove(targetType);
            db.SaveChanges();

            return Ok(targetType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TargetTypeExists(int id)
        {
            return db.TargetType.Count(e => e.Id == id) > 0;
        }
    }
}