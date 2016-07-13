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
    public class TargetsController : ApiController
    {
        private SelorBusiness_SOCEntities db = new SelorBusiness_SOCEntities();

        // GET: api/Targets
        public IQueryable<Target> GetTarget()
        {
            return db.Target;
        }

        // GET: api/Targets/5
        [ResponseType(typeof(Target))]
        public IHttpActionResult GetTarget(int id)
        {
            Target target = db.Target.Find(id);
            if (target == null)
            {
                return NotFound();
            }

            return Ok(target);
        }

        // PUT: api/Targets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarget(int id, Target target)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != target.Id)
            {
                return BadRequest();
            }

            db.Entry(target).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetExists(id))
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

        // POST: api/Targets
        [ResponseType(typeof(Target))]
        public IHttpActionResult PostTarget(Target target)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Target.Add(target);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = target.Id }, target);
        }

        // DELETE: api/Targets/5
        [ResponseType(typeof(Target))]
        public IHttpActionResult DeleteTarget(int id)
        {
            Target target = db.Target.Find(id);
            if (target == null)
            {
                return NotFound();
            }

            db.Target.Remove(target);
            db.SaveChanges();

            return Ok(target);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TargetExists(int id)
        {
            return db.Target.Count(e => e.Id == id) > 0;
        }
    }
}