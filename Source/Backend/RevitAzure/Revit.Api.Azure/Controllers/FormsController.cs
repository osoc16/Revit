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

    [RoutePrefix("api/form")]
    public class FormsController : ApiController
    {
        private DataContext db = new DataContext();

        [Route("juries/{juryId}/forms/{formId}/candidates/{candidatId}")]
        [ResponseType(typeof(DtoForm))]
        // GET: api/juries/{juryId}/forms/{formId}/candidates/{candidatId}
        public object Get(int juryId, int formId, int candidatId, string language = "en")
        {
            Form form = db.Forms.Find(formId);
            if (form == null)
            {
                return NotFound();
            }
            Candidate candidate = db.Candidates.Find(candidatId);
            if (candidate == null)
            {
                return NotFound();
            }

            //return Ok(form);

            DtoForm result = new DtoForm();

            result.candidate = candidate.ToDto();
            result.competencesList = new List<DtoCompetence>();
            result.candidateList = new List<DtoCandidate>();
            result.formID = form.ID;
            result.name =form.name;

            if (form.Scores.Count>0)
            {
                result.score = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId).result;
            }
            else
            {
                result.score = null;
            }

            result.scoreMax =form.finalScoreMax;
            result.scoreMin =form.finalScoreMin;
            if (form.Scores.Count > 0)
            {
                result.total = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId).finalResult;
            }
            else
            {
                result.total = null;
            }

            foreach (var compet in form.Competences)
            {

                DtoCompetence compToAdd = new DtoCompetence();
                compToAdd.dimensions = new List<DtoDimension>();
                compToAdd.competenceID = compet.ID;
                compToAdd.status = compet.status;
                compToAdd.code = compet.code;

                switch (language)
                {
                    case "en":
                        {
                            compToAdd.name = compet.name_EN;
                            compToAdd.statusMessage = compet.statusMessage_EN;
                            compToAdd.description = compet.description_EN;
                            break;
                        }
                    case "fr":
                        {
                            compToAdd.name = compet.name_FR;
                            compToAdd.statusMessage = compet.statusMessage_FR;
                            compToAdd.description = compet.description_FR;

                            break;
                        }
                    case "nl":
                        {
                            compToAdd.name = compet.name_NL;
                            compToAdd.statusMessage = compet.statusMessage_NL;
                            compToAdd.description = compet.description_NL;

                            break;
                        }
                    case "de":
                        {
                            compToAdd.name = compet.name_DE;
                            compToAdd.statusMessage = compet.statusMessage_DE;
                            compToAdd.description = compet.description_DE;

                            break;
                        }
                    default:
                        {
                            compToAdd.name = compet.name_EN;
                            compToAdd.statusMessage = compet.statusMessage_EN;
                            compToAdd.description = compet.description_EN;

                            break;
                        }
                }
                foreach (var dim in compet.Dimensions)
                {
                    DtoDimension dimToAdd = new DtoDimension();
                    dimToAdd = dim.ToDto();

                    if (dim.Scores.Count>0)
                    {
                        dimToAdd.score = dim.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.dimensionId == dim.ID).result;
                    }
                        if(dimToAdd.score==null)
                            dimToAdd.notObserved = true;

                        
                        compToAdd.dimensions.Add(dimToAdd);
                }

                
                result.competencesList.Add(compToAdd);
            }
            foreach (var candi in form.Candidates)
            {
                
                result.candidateList.Add(candi.ToDto());
            }

            return Ok(result);
        }



        //POST: 
        [ResponseType(typeof(Form))]
        public IHttpActionResult Post(DtoForm formNew)
        {
            Form formDb = new Form();
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            formDb = formNew.ToEntity(false);

            //competence region
            #region competences
            foreach (var comp in formNew.competencesList)
            {
                Competence compDb = db.Competences.Find(comp.competenceID);


                if (compDb == null)
                {
                    compDb = new Competence();

                    compDb = comp.ToEntity(false);

                }
                

                //dimension region
                #region dimension
                foreach (var dim in comp.dimensions)
                {
                    Dimension dimDb = db.Dimensions.Find(dim.dimensionID);

                    if (dimDb == null)
                    {
                        dimDb = new Dimension();
                        dimDb = dim.ToEntity(); ;
                        compDb.Dimensions.Add(dimDb);
                    }
                    else if (compDb.Dimensions.Contains(dimDb) == false)
                    {
                        compDb.Dimensions.Add(dimDb);
                    }
                }
                #endregion

                formDb.Competences.Add(compDb);

            }

            #endregion

            //jury region
            #region jury
            if (formNew.juryList != null)
                foreach (var jury in formNew.juryList)
                {
                    Jury juryDb = db.Juries.Find(jury.juryId);

                    if (juryDb == null)
                    {
                        juryDb = new Jury();
                        juryDb= jury.ToEntity();

                        formDb.Juries.Add(juryDb);
                    }
                    else if (formDb.Juries.Contains(juryDb) == false)
                        formDb.Juries.Add(juryDb);
                }
            #endregion

            //candidate region
            #region candidates
            if (formNew.candidateList == null)
                formNew.candidateList = new List<DtoCandidate>();
            foreach (var candi in formNew.candidateList)
            {
                Candidate canDb = db.Candidates.Find(candi.candidateID);
                
                if (canDb == null)
                {
                    canDb = new Candidate();
                    canDb = candi.ToEntity();
                    db.Candidates.Add(canDb);
                    db.SaveChanges();
                    canDb = db.Candidates.Where(c=> c.lastname==canDb.lastname).Where(c => c.firstname == canDb.firstname).Last();                   
                }
                
                if (candi.juries != null)
                    foreach (var jur in candi.juries)
                    {
                        juryCandidateForm JCF = new juryCandidateForm();
                        JCF.candidate_ID = candi.candidateID;
                        JCF.form_ID = formDb.ID;
                        JCF.jury_ID = jur.juryId;
                        db.JuryCandidateForms.Add(JCF);
                    }

                if (formDb.Candidates == null)
                    formDb.Candidates = new List<Candidate>();
                formDb.Candidates.Add(canDb);
            }

            db.Forms.Add(formDb);
            db.SaveChanges();
            formDb = db.Forms.Where(f=> f.name==formDb.name).Last();

            foreach (var candi in formNew.candidateList)
            {
                Candidate canDb = db.Candidates.Find(candi.candidateID);

                if (candi.juries != null)
                    foreach (var jur in candi.juries)
                    {
                        juryCandidateForm JCF = new juryCandidateForm();
                        JCF.candidate_ID = candi.candidateID;
                        JCF.form_ID = formDb.ID;
                        JCF.jury_ID = jur.juryId;
                        db.JuryCandidateForms.Add(JCF);
                    }

            }
            #endregion

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = formDb.ID }, formDb);
        }

        //PUT: 
        [ResponseType(typeof(Form))]
        public IHttpActionResult Put(DtoForm formNew)
        {
            Form formDb = db.Forms.Find(formNew.formID);

            if (formDb == null)        
            {
                formDb = new Form();

                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }
            }
            else
            {
                db.Entry(formDb).State = EntityState.Modified;
            }
            formDb = formNew.ToEntity(false);

            //competence region
            #region competences
            foreach (var comp in formNew.competencesList)
            {
                Competence compDb = db.Competences.Find(comp.competenceID);


                if (compDb == null)
                {
                    compDb = new Competence();

                    compDb = comp.ToEntity(false);

                }


                //dimension region
                #region dimension
                foreach (var dim in comp.dimensions)
                {
                    Dimension dimDb = db.Dimensions.Find(dim.dimensionID);

                    if (dimDb == null)
                    {
                        dimDb = new Dimension();
                        dimDb = dim.ToEntity(); ;
                        compDb.Dimensions.Add(dimDb);
                    }
                    else if (compDb.Dimensions.Contains(dimDb) == false)
                    {
                        compDb.Dimensions.Add(dimDb);
                    }
                }
                #endregion

                formDb.Competences.Add(compDb);

            }

            #endregion

            //jury region
            #region jury
            if (formNew.juryList != null)
                foreach (var jury in formNew.juryList)
                {
                    Jury juryDb = db.Juries.Find(jury.juryId);

                    if (juryDb == null)
                    {
                        juryDb = new Jury();
                        juryDb = jury.ToEntity();

                        formDb.Juries.Add(juryDb);
                    }
                    else if (formDb.Juries.Contains(juryDb) == false)
                        formDb.Juries.Add(juryDb);
                }
            #endregion

            //candidate region
            #region candidates
            if (formNew.candidateList == null)
                formNew.candidateList = new List<DtoCandidate>();
            foreach (var candi in formNew.candidateList)
            {
                Candidate canDb = db.Candidates.Find(candi.candidateID);

                if (canDb == null)
                {
                    canDb = new Candidate();
                    canDb = candi.ToEntity();
                    db.Candidates.Add(canDb);
                    db.SaveChanges();
                    canDb = db.Candidates.Where(c => c.lastname == canDb.lastname).Where(c => c.firstname == canDb.firstname).Last();
                }

                if (candi.juries != null)
                    foreach (var jur in candi.juries)
                    {
                        juryCandidateForm JCF = new juryCandidateForm();
                        JCF.candidate_ID = candi.candidateID;
                        JCF.form_ID = formDb.ID;
                        JCF.jury_ID = jur.juryId;
                        db.JuryCandidateForms.Add(JCF);
                    }

                if (formDb.Candidates == null)
                    formDb.Candidates = new List<Candidate>();
                formDb.Candidates.Add(canDb);
            }

            db.Forms.Add(formDb);
            db.SaveChanges();
            formDb = db.Forms.Where(f => f.name == formDb.name).Last();

            foreach (var candi in formNew.candidateList)
            {
                Candidate canDb = db.Candidates.Find(candi.candidateID);

                if (candi.juries != null)
                    foreach (var jur in candi.juries)
                    {
                        juryCandidateForm JCF = new juryCandidateForm();
                        JCF.candidate_ID = candi.candidateID;
                        JCF.form_ID = formDb.ID;
                        JCF.jury_ID = jur.juryId;
                        db.JuryCandidateForms.Add(JCF);
                    }

            }
            #endregion

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = formDb.ID }, formDb);
        }
















        // GET: api/Forms
        public IQueryable<Form> GetForms()
        {
            return db.Forms;
        }

        // GET: api/Forms/5
        [ResponseType(typeof(Form))]
        public IHttpActionResult GetForm(int id)
        {
            Form form = db.Forms.Find(id);
            if (form == null)
            {
                return NotFound();
            }

            return Ok(form);
        }

        // PUT: api/Forms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutForm(int id, Form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != form.ID)
            {
                return BadRequest();
            }

            db.Entry(form).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormExists(id))
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

        // POST: api/Forms
        //[ResponseType(typeof(Form))]
        //public IHttpActionResult PostForm(Form form)
        //{
        //    if (!ModelState.IsValid)
        //    {

        //        return BadRequest(ModelState);
        //    }

        //    db.Forms.Add(form);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = form.ID }, form);
        //}

        // DELETE: api/Forms/5
        [ResponseType(typeof(Form))]
        public IHttpActionResult DeleteForm(int id)
        {
            Form form = db.Forms.Find(id);
            if (form == null)
            {
                return NotFound();
            }

            db.Forms.Remove(form);
            db.SaveChanges();

            return Ok(form);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FormExists(int id)
        {
            return db.Forms.Count(e => e.ID == id) > 0;
        }
    }
}