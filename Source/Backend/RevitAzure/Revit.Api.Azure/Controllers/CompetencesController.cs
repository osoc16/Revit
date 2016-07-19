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
using Revit.Api.Azure.Models;

namespace Revit.Api.Azure.Controllers
{
    public class CompetencesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Competences
        public IQueryable<Competence> GetCompetences()
        {
            return db.Competences;
        }

        // GET: api/Competences/5
        [ResponseType(typeof(Competence))]
        public IHttpActionResult GetCompetence(int id)
        {
            Competence competence = db.Competences.Find(id);
            if (competence == null)
            {
                return NotFound();
            }

            return Ok(competence);
        }

        // PUT: api/Competences/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompetence(int id, Competence competence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != competence.ID)
            {
                return BadRequest();
            }

            db.Entry(competence).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetenceExists(id))
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

        // POST: api/Competences
        [ResponseType(typeof(Competence))]
        public IHttpActionResult PostCompetence(Competence competence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Competences.Add(competence);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = competence.ID }, competence);
        }

        // DELETE: api/Competences/5
        [ResponseType(typeof(Competence))]
        public IHttpActionResult DeleteCompetence(int id)
        {
            Competence competence = db.Competences.Find(id);
            if (competence == null)
            {
                return NotFound();
            }

            db.Competences.Remove(competence);
            db.SaveChanges();

            return Ok(competence);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompetenceExists(int id)
        {
            return db.Competences.Count(e => e.ID == id) > 0;
        }
    }
}