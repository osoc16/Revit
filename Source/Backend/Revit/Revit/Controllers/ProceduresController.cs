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
    public class ProceduresController : ApiController
    {
        private SelorBusiness_SOCEntities db = new SelorBusiness_SOCEntities();

        // GET: api/Procedures
        public IQueryable<Procedure> GetProcedure()
        {
            return db.Procedure;
        }

        // GET: api/Procedures/5
        [ResponseType(typeof(Procedure))]
        public IHttpActionResult GetProcedure(int id)
        {
            Procedure procedure = db.Procedure.Find(id);
            if (procedure == null)
            {
                return NotFound();
            }

            return Ok(procedure);
        }

        // PUT: api/Procedures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProcedure(int id, Procedure procedure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != procedure.Id)
            {
                return BadRequest();
            }

            db.Entry(procedure).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcedureExists(id))
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

        // POST: api/Procedures
        [ResponseType(typeof(Procedure))]
        public IHttpActionResult PostProcedure(Procedure procedure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Procedure.Add(procedure);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = procedure.Id }, procedure);
        }

        // DELETE: api/Procedures/5
        [ResponseType(typeof(Procedure))]
        public IHttpActionResult DeleteProcedure(int id)
        {
            Procedure procedure = db.Procedure.Find(id);
            if (procedure == null)
            {
                return NotFound();
            }

            db.Procedure.Remove(procedure);
            db.SaveChanges();

            return Ok(procedure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProcedureExists(int id)
        {
            return db.Procedure.Count(e => e.Id == id) > 0;
        }
    }
}