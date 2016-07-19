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
using RevitApi.Models;

namespace RevitApi.Controllers
{
    public class ScreeningsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Screenings
        public IQueryable<Screening> GetScreenings()
        {
            return db.Screenings;
        }

        // GET: api/Screenings/5
        [ResponseType(typeof(Screening))]
        public IHttpActionResult GetScreening(int id)
        {
            Screening screening = db.Screenings.Find(id);
            if (screening == null)
            {
                return NotFound();
            }

            return Ok(screening);
        }

        // PUT: api/Screenings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScreening(int id, Screening screening)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != screening.ID)
            {
                return BadRequest();
            }

            db.Entry(screening).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreeningExists(id))
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

        // POST: api/Screenings
        [ResponseType(typeof(Screening))]
        public IHttpActionResult PostScreening(Screening screening)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Screenings.Add(screening);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = screening.ID }, screening);
        }

        // DELETE: api/Screenings/5
        [ResponseType(typeof(Screening))]
        public IHttpActionResult DeleteScreening(int id)
        {
            Screening screening = db.Screenings.Find(id);
            if (screening == null)
            {
                return NotFound();
            }

            db.Screenings.Remove(screening);
            db.SaveChanges();

            return Ok(screening);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScreeningExists(int id)
        {
            return db.Screenings.Count(e => e.ID == id) > 0;
        }
    }
}