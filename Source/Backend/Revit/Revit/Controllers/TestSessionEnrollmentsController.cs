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
    public class TestSessionEnrollmentsController : ApiController
    {
        private SelorBusiness_SOCEntities db = new SelorBusiness_SOCEntities();

        // GET: api/TestSessionEnrollments
        public IQueryable<TestSessionEnrollment> GetTestSessionEnrollment()
        {
            return db.TestSessionEnrollment;
        }

        // GET: api/TestSessionEnrollments/5
        [ResponseType(typeof(TestSessionEnrollment))]
        public IHttpActionResult GetTestSessionEnrollment(int id)
        {
            TestSessionEnrollment testSessionEnrollment = db.TestSessionEnrollment.Find(id);
            if (testSessionEnrollment == null)
            {
                return NotFound();
            }

            return Ok(testSessionEnrollment);
        }

        // PUT: api/TestSessionEnrollments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTestSessionEnrollment(int id, TestSessionEnrollment testSessionEnrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != testSessionEnrollment.Id)
            {
                return BadRequest();
            }

            db.Entry(testSessionEnrollment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestSessionEnrollmentExists(id))
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

        // POST: api/TestSessionEnrollments
        [ResponseType(typeof(TestSessionEnrollment))]
        public IHttpActionResult PostTestSessionEnrollment(TestSessionEnrollment testSessionEnrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TestSessionEnrollment.Add(testSessionEnrollment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = testSessionEnrollment.Id }, testSessionEnrollment);
        }

        // DELETE: api/TestSessionEnrollments/5
        [ResponseType(typeof(TestSessionEnrollment))]
        public IHttpActionResult DeleteTestSessionEnrollment(int id)
        {
            TestSessionEnrollment testSessionEnrollment = db.TestSessionEnrollment.Find(id);
            if (testSessionEnrollment == null)
            {
                return NotFound();
            }

            db.TestSessionEnrollment.Remove(testSessionEnrollment);
            db.SaveChanges();

            return Ok(testSessionEnrollment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestSessionEnrollmentExists(int id)
        {
            return db.TestSessionEnrollment.Count(e => e.Id == id) > 0;
        }
    }
}