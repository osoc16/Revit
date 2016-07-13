using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RevitApi.Models
{
    public class DimensionsController : Controller
    {
        DataContext context = new DataContext();

        // GET: Dimensions
        public ActionResult Index()
        {
            return View(context.Dimensions.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost()]
        public ActionResult Create(Dimension dimension)
        {
            try
            {
                if (!ModelState.IsValid) return View(dimension);

                context.Dimensions.Add(dimension);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
           
            catch (Exception e)
            {
                
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;

            }
            
        }

        public ActionResult Edit()
        {
            return View();
        }


        public ActionResult Delete()
        {
            return View();
        }
    }
}