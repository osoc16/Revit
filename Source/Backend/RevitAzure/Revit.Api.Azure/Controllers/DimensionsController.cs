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
using Revit.Api.Azure.DTO;

namespace Revit.Api.Azure.Controllers
{
    [RoutePrefix("api/dim")]
    public class DimensionsController : ApiController
    {
        private DataContext db = new DataContext();


        [Route("{id}")]
        [ResponseType(typeof(Dimension))]
        // GET: api
        public object Get(int id, string language = "en")
        {


            Dimension dimension = db.Dimensions.Find(id);
            DtoDimension dim = new DtoDimension();


            if (dimension == null)
            {
                return NotFound();
            }
            dim = dimension.ToDto(language);

            return Ok(dim);
        }
            
        



        // GET: api/Dimensions
        public IQueryable<Dimension> GetDimensions()
        {
            return db.Dimensions;
        }

        // GET: api/Dimensions/5
        [ResponseType(typeof(Dimension))]
        public IHttpActionResult GetDimension(int id)
        {
            Dimension dimension = db.Dimensions.Find(id);
   
            if (dimension == null)
            {
                return NotFound();
            }

            return Ok(dimension);
        }

        // PUT: api/Dimensions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDimension(int id, Dimension dimension)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dimension.ID)
            {
                return BadRequest();
            }

            db.Entry(dimension).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimensionExists(id))
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

        // POST: api/Dimensions
        [ResponseType(typeof(Dimension))]
        public IHttpActionResult PostDimension(Dimension dimension)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dimensions.Add(dimension);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dimension.ID }, dimension);
        }

        // DELETE: api/Dimensions/5
        [ResponseType(typeof(Dimension))]
        public IHttpActionResult DeleteDimension(int id)
        {
            Dimension dimension = db.Dimensions.Find(id);
            if (dimension == null)
            {
                return NotFound();
            }

            db.Dimensions.Remove(dimension);
            db.SaveChanges();

            return Ok(dimension);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DimensionExists(int id)
        {
            return db.Dimensions.Count(e => e.ID == id) > 0;
        }
    }
}