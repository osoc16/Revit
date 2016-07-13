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
    public class TargetResultsController : ApiController
    {
        private SelorBusiness_SOCEntities db = new SelorBusiness_SOCEntities();

        // GET: api/TargetResults
        public IQueryable<TargetResult> GetTargetResult()
        {
            return db.TargetResult;
        }

        // GET: api/TargetResults/5
        [ResponseType(typeof(TargetResult))]
        public IHttpActionResult GetTargetResult(int id)
        {
            TargetResult targetResult = db.TargetResult.Find(id);
            if (targetResult == null)
            {
                return NotFound();
            }

            return Ok(targetResult);
        }

        // PUT: api/TargetResults/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTargetResult(int id, TargetResult targetResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != targetResult.Id)
            {
                return BadRequest();
            }

            db.Entry(targetResult).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetResultExists(id))
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

        // POST: api/TargetResults
        [ResponseType(typeof(TargetResult))]
        public IHttpActionResult PostTargetResult(TargetResult targetResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TargetResult.Add(targetResult);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = targetResult.Id }, targetResult);
        }

        // DELETE: api/TargetResults/5
        [ResponseType(typeof(TargetResult))]
        public IHttpActionResult DeleteTargetResult(int id)
        {
            TargetResult targetResult = db.TargetResult.Find(id);
            if (targetResult == null)
            {
                return NotFound();
            }

            db.TargetResult.Remove(targetResult);
            db.SaveChanges();

            return Ok(targetResult);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TargetResultExists(int id)
        {
            return db.TargetResult.Count(e => e.Id == id) > 0;
        }
    }
}