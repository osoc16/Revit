﻿using System;
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
using Revit.Api.Azure.DTO;

namespace Revit.Api.Azure.Controllers
{


    [RoutePrefix("api")]
    public class JuriesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Juries
        public IQueryable<Jury> GetJuries()
        {
            return db.Juries;
        }

        // GET: api/Juries/?search= ***
        [ResponseType(typeof(DtoJury))]
        public IHttpActionResult GetJury(string search)
        {
            ICollection<Jury> juries = db.Juries.Where(j => j.firstname.Contains(search)|| j.lastname .Contains(search)).ToList() ;
            ICollection<DtoJury> juriesToSend = new List<DtoJury>();
            if (juries == null)
            {
                return NotFound();
            }
            foreach (var jury in juries)
            {
                DtoJury tempJury = new DtoJury();
                tempJury.juryId = jury.ID;
                tempJury.name = jury.lastname + " " + jury.firstname;
                tempJury.firstName = jury.firstname;
                tempJury.lastName = jury.lastname;
                juriesToSend.Add(tempJury);
            }
            return Ok(juriesToSend);
        }


        // GET: api/Juries/5/forms
        [Route("juries/{id}/forms")]
        [ResponseType(typeof(Jury))]
        public IHttpActionResult GetJury(int id)
        {
            //Jury jury = db.Juries.Find(id);
            //if (jury == null)
            //{
            //    return NotFound();
            //}

            List<DtoForm> result = new List<DtoForm>();
            //foreach (var item in db.Forms.(o => o.Juries.ToList().Where(j => j.ID == id)));
            //{
            //    result.Add(item.ToDto());
            //}

            var formsForJury = db.Forms.Where(f => f.Juries.Any(j => j.ID == id));

            foreach ( var fj in formsForJury)
            {

                result.Add(fj.ToDto());
            }


            return Ok(result);
        }














        //// GET: api/Juries/5
        //[ResponseType(typeof(Jury))]
        //public IHttpActionResult GetJury(int id)
        //{
        //    Jury jury = db.Juries.Find(id);
        //    if (jury == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(jury);
        //}

        // PUT: api/Juries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJury(int id, Jury jury)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jury.ID)
            {
                return BadRequest();
            }

            db.Entry(jury).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JuryExists(id))
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

        // POST: api/Juries
        [ResponseType(typeof(Jury))]
        public IHttpActionResult PostJury(Jury jury)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Juries.Add(jury);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jury.ID }, jury);
        }

        // DELETE: api/Juries/5
        [ResponseType(typeof(Jury))]
        public IHttpActionResult DeleteJury(int id)
        {
            Jury jury = db.Juries.Find(id);
            if (jury == null)
            {
                return NotFound();
            }

            db.Juries.Remove(jury);
            db.SaveChanges();

            return Ok(jury);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JuryExists(int id)
        {
            return db.Juries.Count(e => e.ID == id) > 0;
        }
    }
}