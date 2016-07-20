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
            DtoCandidate cand = new DtoCandidate();
            cand.candidateID = candidate.ID;
            cand.name= candidate.lastname + " " + candidate.firstname;
            DtoForm result = new DtoForm();
            result.competences = new List<DtoCompetences>();
            result.candidateList = new List<DtoCandidate>();
            result.formID = form.ID;
            result.name =form.Name;
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
            result.candidate = cand;
            foreach (var compet in form.Competences)
            {
                DtoCompetences compToAdd = new DtoCompetences();
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
                    dimToAdd.dimensionID = dim.ID;
                    if (dim.Scores.Count>0)
                    {
                        dimToAdd.score = dim.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.dimensionId == dim.ID).result;
                    }
                            if(dimToAdd.score==null)
                                dimToAdd.notObserved = true;

                            switch (language)
                            {
                                case "en":
                                    {
                                        dimToAdd.name = dim.name_EN;
                                        dimToAdd.description = dim.description_EN;

                                        break;
                                    }
                                case "fr":
                                    {
                                        dimToAdd.name = dim.name_FR;
                                        dimToAdd.description = dim.description_FR;

                                        break;
                                    }
                                case "nl":
                                    {
                                        dimToAdd.name = dim.name_NL;
                                        dimToAdd.description = dim.description_NL;

                                        break;
                                    }
                                case "de":
                                    {
                                        dimToAdd.name = dim.name_DE;
                                        dimToAdd.description = dim.description_DE;

                                        break;
                                    }
                                default:
                                    {
                                        dimToAdd.name = dim.name_EN;
                                        dimToAdd.description = dim.description_EN;

                                        break;
                                    }
                            }
                            compToAdd.dimensions.Add(dimToAdd);
                        }

                
                result.competences.Add(compToAdd);
            }
            foreach (var candi in form.Candidates)
            {
                DtoCandidate candToAdd = new DtoCandidate();
                candToAdd.candidateID = candi.ID;
                candToAdd.name = candi.lastname + " " + candi.firstname;
                result.candidateList.Add(candToAdd);
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

            formDb.Name = formNew.name;

            formDb.description_EN = formNew.description_EN;
            formDb.description_FR = formNew.description_FR;
            formDb.description_NL = formNew.description_NL;
            formDb.description_DE = formNew.description_DE;
            formDb.finalScoreMax = formNew.finalScoreMax;
            formDb.finalScoreMin = formNew.finalScoreMin;

            //competence region
            #region competences
            foreach (var comp in formNew.competences)
            {
                Competence compDb = db.Competences.Find(comp.competenceID);

                if (compDb == null)
                {
                    compDb = new Competence();

                    compDb.status = comp.status;
                    compDb.code = comp.code;

                    compDb.name_EN = comp.name_EN;
                    compDb.name_FR = comp.name_FR;
                    compDb.name_NL = comp.name_NL;
                    compDb.name_DE = comp.name_DE;

                    compDb.description_EN = comp.description_EN;
                    compDb.description_FR = comp.description_FR;
                    compDb.description_NL = comp.description_NL;
                    compDb.description_DE = comp.description_DE;

                    compDb.statusMessage_EN = comp.statusMessage_EN;
                    compDb.statusMessage_FR = comp.statusMessage_FR;
                    compDb.statusMessage_DE = comp.statusMessage_DE;
                    compDb.statusMessage_NL = comp.statusMessage_NL;

                }

                if (compDb.Dimensions == null)
                {
                    compDb.Dimensions = new List<Dimension>();
                }




                //dimension region
                #region dimension
                foreach (var dim in comp.dimensions)
                {
                    Dimension dimDb = db.Dimensions.Find(dim.dimensionID);

                    if (compDb == null)
                    {
                        dimDb = new Dimension();
                        dimDb.name_EN = dim.name_EN;
                        dimDb.name_FR = dim.name_FR;
                        dimDb.name_NL = dim.name_NL;
                        dimDb.name_DE = dim.name_DE;
                        dimDb.code = dim.code;
                        compDb.Dimensions.Add(dimDb);

                    }
                    else if (compDb.Dimensions.Contains(dimDb) == false)
                    {
                        compDb.Dimensions.Add(dimDb);
                    }
                }
                #endregion

                if (formDb.Competences == null)
                    formDb.Competences = new List<Competence>();
                formDb.Competences.Add(compDb);
            #endregion

            }

            //jury region
            #region jury
            if (formNew.juryList != null)
                foreach (var jury in formNew.juryList)
                {
                    Jury juryDb = db.Juries.Find(jury.juryId);
                    if (formDb.Juries == null)
                    {
                        formDb.Juries = new List<Jury>();
                    }
                    if (juryDb == null)
                    {
                        juryDb = new Jury();
                        juryDb.lastname = jury.lastname;
                        juryDb.firstname = jury.firstname;

                        formDb.Juries.Add(juryDb);
                    }
                    if (formDb.Juries.Contains(juryDb) == false)
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
                    canDb.lastname = candi.lastname;
                    canDb.firstname = candi.firstname;

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
            formDb = db.Forms.Where(f=> f.Name==formDb.Name).Last();

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