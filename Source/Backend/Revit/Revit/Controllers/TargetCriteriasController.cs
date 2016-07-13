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
    public class TargetCriteriasController : ApiController
    {
        private SelorBusiness_SOCEntities db = new SelorBusiness_SOCEntities();

        // GET: api/TargetCriterias
        public IQueryable<TargetCriteria> GetTargetCriteria()
        {
            return db.TargetCriteria;
        }

        // GET: api/TargetCriterias/5
        [ResponseType(typeof(TargetCriteria))]
        public IHttpActionResult GetTargetCriteria(int id)
        {
            TargetCriteria targetCriteria = db.TargetCriteria.Find(id);
            if (targetCriteria == null)
            {
                return NotFound();
            }

            return Ok(targetCriteria);
        }

        // PUT: api/TargetCriterias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTargetCriteria(int id, TargetCriteria targetCriteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != targetCriteria.Id)
            {
                return BadRequest();
            }

            db.Entry(targetCriteria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetCriteriaExists(id))
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

        // POST: api/TargetCriterias
        [ResponseType(typeof(TargetCriteria))]
        public IHttpActionResult PostTargetCriteria(TargetCriteria targetCriteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TargetCriteria.Add(targetCriteria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = targetCriteria.Id }, targetCriteria);
        }

        // DELETE: api/TargetCriterias/5
        [ResponseType(typeof(TargetCriteria))]
        public IHttpActionResult DeleteTargetCriteria(int id)
        {
            TargetCriteria targetCriteria = db.TargetCriteria.Find(id);
            if (targetCriteria == null)
            {
                return NotFound();
            }

            db.TargetCriteria.Remove(targetCriteria);
            db.SaveChanges();

            return Ok(targetCriteria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TargetCriteriaExists(int id)
        {
            return db.TargetCriteria.Count(e => e.Id == id) > 0;
        }
    }
}