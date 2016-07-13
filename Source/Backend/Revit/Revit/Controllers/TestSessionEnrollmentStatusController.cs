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
    public class TestSessionEnrollmentStatusController : ApiController
    {
        private SelorBusiness_SOCEntities db = new SelorBusiness_SOCEntities();

        // GET: api/TestSessionEnrollmentStatus
        public IQueryable<TestSessionEnrollmentStatus> GetTestSessionEnrollmentStatus()
        {
            return db.TestSessionEnrollmentStatus;
        }

        // GET: api/TestSessionEnrollmentStatus/5
        [ResponseType(typeof(TestSessionEnrollmentStatus))]
        public IHttpActionResult GetTestSessionEnrollmentStatus(int id)
        {
            TestSessionEnrollmentStatus testSessionEnrollmentStatus = db.TestSessionEnrollmentStatus.Find(id);
            if (testSessionEnrollmentStatus == null)
            {
                return NotFound();
            }

            return Ok(testSessionEnrollmentStatus);
        }

        // PUT: api/TestSessionEnrollmentStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTestSessionEnrollmentStatus(int id, TestSessionEnrollmentStatus testSessionEnrollmentStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != testSessionEnrollmentStatus.Id)
            {
                return BadRequest();
            }

            db.Entry(testSessionEnrollmentStatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestSessionEnrollmentStatusExists(id))
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

        // POST: api/TestSessionEnrollmentStatus
        [ResponseType(typeof(TestSessionEnrollmentStatus))]
        public IHttpActionResult PostTestSessionEnrollmentStatus(TestSessionEnrollmentStatus testSessionEnrollmentStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TestSessionEnrollmentStatus.Add(testSessionEnrollmentStatus);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TestSessionEnrollmentStatusExists(testSessionEnrollmentStatus.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = testSessionEnrollmentStatus.Id }, testSessionEnrollmentStatus);
        }

        // DELETE: api/TestSessionEnrollmentStatus/5
        [ResponseType(typeof(TestSessionEnrollmentStatus))]
        public IHttpActionResult DeleteTestSessionEnrollmentStatus(int id)
        {
            TestSessionEnrollmentStatus testSessionEnrollmentStatus = db.TestSessionEnrollmentStatus.Find(id);
            if (testSessionEnrollmentStatus == null)
            {
                return NotFound();
            }

            db.TestSessionEnrollmentStatus.Remove(testSessionEnrollmentStatus);
            db.SaveChanges();

            return Ok(testSessionEnrollmentStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestSessionEnrollmentStatusExists(int id)
        {
            return db.TestSessionEnrollmentStatus.Count(e => e.Id == id) > 0;
        }
    }
}