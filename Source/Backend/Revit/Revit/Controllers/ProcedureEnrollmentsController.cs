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
    public class ProcedureEnrollmentsController : ApiController
    {
        private SelorBusiness_SOCEntities db = new SelorBusiness_SOCEntities();

        // GET: api/ProcedureEnrollments
        public IQueryable<ProcedureEnrollment> GetProcedureEnrollment()
        {
            return db.ProcedureEnrollment;
        }

        // GET: api/ProcedureEnrollments/5
        [ResponseType(typeof(ProcedureEnrollment))]
        public IHttpActionResult GetProcedureEnrollment(int id)
        {
            ProcedureEnrollment procedureEnrollment = db.ProcedureEnrollment.Find(id);
            if (procedureEnrollment == null)
            {
                return NotFound();
            }

            return Ok(procedureEnrollment);
        }

        // PUT: api/ProcedureEnrollments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProcedureEnrollment(int id, ProcedureEnrollment procedureEnrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != procedureEnrollment.Id)
            {
                return BadRequest();
            }

            db.Entry(procedureEnrollment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcedureEnrollmentExists(id))
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

        // POST: api/ProcedureEnrollments
        [ResponseType(typeof(ProcedureEnrollment))]
        public IHttpActionResult PostProcedureEnrollment(ProcedureEnrollment procedureEnrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProcedureEnrollment.Add(procedureEnrollment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = procedureEnrollment.Id }, procedureEnrollment);
        }

        // DELETE: api/ProcedureEnrollments/5
        [ResponseType(typeof(ProcedureEnrollment))]
        public IHttpActionResult DeleteProcedureEnrollment(int id)
        {
            ProcedureEnrollment procedureEnrollment = db.ProcedureEnrollment.Find(id);
            if (procedureEnrollment == null)
            {
                return NotFound();
            }

            db.ProcedureEnrollment.Remove(procedureEnrollment);
            db.SaveChanges();

            return Ok(procedureEnrollment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProcedureEnrollmentExists(int id)
        {
            return db.ProcedureEnrollment.Count(e => e.Id == id) > 0;
        }
    }
}