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
    public class ProcedureEnrollmentStatusController : ApiController
    {
        private SelorBusiness_SOCEntities db = new SelorBusiness_SOCEntities();

        // GET: api/ProcedureEnrollmentStatus
        public IQueryable<ProcedureEnrollmentStatus> GetProcedureEnrollmentStatus()
        {
            return db.ProcedureEnrollmentStatus;
        }

        // GET: api/ProcedureEnrollmentStatus/5
        [ResponseType(typeof(ProcedureEnrollmentStatus))]
        public IHttpActionResult GetProcedureEnrollmentStatus(int id)
        {
            ProcedureEnrollmentStatus procedureEnrollmentStatus = db.ProcedureEnrollmentStatus.Find(id);
            if (procedureEnrollmentStatus == null)
            {
                return NotFound();
            }

            return Ok(procedureEnrollmentStatus);
        }

        // PUT: api/ProcedureEnrollmentStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProcedureEnrollmentStatus(int id, ProcedureEnrollmentStatus procedureEnrollmentStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != procedureEnrollmentStatus.Id)
            {
                return BadRequest();
            }

            db.Entry(procedureEnrollmentStatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcedureEnrollmentStatusExists(id))
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

        // POST: api/ProcedureEnrollmentStatus
        [ResponseType(typeof(ProcedureEnrollmentStatus))]
        public IHttpActionResult PostProcedureEnrollmentStatus(ProcedureEnrollmentStatus procedureEnrollmentStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProcedureEnrollmentStatus.Add(procedureEnrollmentStatus);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProcedureEnrollmentStatusExists(procedureEnrollmentStatus.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = procedureEnrollmentStatus.Id }, procedureEnrollmentStatus);
        }

        // DELETE: api/ProcedureEnrollmentStatus/5
        [ResponseType(typeof(ProcedureEnrollmentStatus))]
        public IHttpActionResult DeleteProcedureEnrollmentStatus(int id)
        {
            ProcedureEnrollmentStatus procedureEnrollmentStatus = db.ProcedureEnrollmentStatus.Find(id);
            if (procedureEnrollmentStatus == null)
            {
                return NotFound();
            }

            db.ProcedureEnrollmentStatus.Remove(procedureEnrollmentStatus);
            db.SaveChanges();

            return Ok(procedureEnrollmentStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProcedureEnrollmentStatusExists(int id)
        {
            return db.ProcedureEnrollmentStatus.Count(e => e.Id == id) > 0;
        }
    }
}