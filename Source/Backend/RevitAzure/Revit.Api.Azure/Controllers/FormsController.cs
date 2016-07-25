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
using System.Web.Http.Cors;

namespace Revit.Api.Azure.Controllers
{

    [RoutePrefix("api")]
    public class FormsController : ApiController
    {
        private DataContext db = new DataContext();
        
        [Route("evaluations/juries/{juryId}/forms/{formId}/candidates/{candidatId}")]
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



            DtoForm result = new DtoForm();

            result = form.ToDto(false);

            result.candidate = candidate.ToDto();



            if (result.candidate.juries ==null)
            {
                result.candidate.juries = new List<DtoJury>();
            }
            foreach (var item in db.JuryCandidateForms.Where(o => o.candidate_ID == candidatId && o.form_ID == formId))
            {
                foreach (var j in db.Juries.Where(j => j.ID == item.jury_ID).ToList())
                {

                    result.candidate.juries.Add(j.ToDto());
                }
            }
            
            if (form.Scores.Count>0)
            {
                if (form.Scores.Count(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null)!=0)
                {
                    result.score = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null).result;
                }

            }
            else
            {
                result.score = null;
            }

            if (form.Scores.Count > 0)
            {
                if (form.Scores.Count(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null) != 0)
                {
                    result.finalScore = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null).finalResult;

                }
            }
            else
            {
                result.score = null;
            }
            result.scoreMax =form.finalScoreMax;
            result.scoreMin =form.finalScoreMin;
            if (form.Scores.Count > 0)
            {
                if (form.Scores.Count(o => o.candidateId == candidatId && o.formId == formId) != 0)
                {
                    result.total = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId).finalResult;
                }
            }
            else
            {
                result.total = null;
            }

            foreach (var compet in form.Competences)
            {
                result.competencesList.Add(compet.ToDto(language));
                foreach (var dim in compet.Dimensions)
                {

                    if (dim.Scores.Count > 0)
                    {
                        if (dim.Scores.Count(o => o.candidateId == candidatId && o.formId == formId && o.dimensionId == dim.ID) != 0)
                        {
                            result.competencesList.Last().dimensions.Last().score = dim.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.dimensionId == dim.ID).result;
                        }
                    }
                    else
                    {
                        result.competencesList.Last().dimensions.Last().notObserved = true;
                    }
                }
            }
            foreach (var candi in form.Candidates)
            {
                
                result.candidateList.Add(candi.ToDto());
            }

            foreach (var c in result.candidateList)
            {
                if (c.juries == null)
                {
                    c.juries = new List<DtoJury>();
                }
                foreach (var item in db.JuryCandidateForms.Where(o => o.candidate_ID == c.candidateId && o.form_ID == formId))
                {
                    foreach (var j in db.Juries.Where(j => j.ID == item.jury_ID).ToList())
                    {

                        c.juries.Add(j.ToDto());
                    }
                }
            }

            return Ok(result);
        }







        [Route("evaluations/juries/{juryId}/forms/{formId}/candidates/{candidatId}")]
        [ResponseType(typeof(DtoForm))]
        // PUT: api/juries/{juryId}/forms/{formId}/candidates/{candidatId}
        public object Put(int juryId, int formId, int candidatId, [FromBody] DtoForm data, string language = "en")
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


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (data == null)
            {
                return BadRequest();
            }
            if (formId != data.formId)
            {
                return BadRequest();
            }

            db.Entry(data.ToEntity()).State = EntityState.Modified;
            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormExists(formId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);




            //DtoForm result = new DtoForm();

            //result = form.ToDto(false);

            //result.candidate = candidate.ToDto();
            //if (form.Scores.Count > 0)
            //{
            //    if (form.Scores.Count(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null) != 0)
            //    {
            //        result.score = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null).result;
            //    }

            //}
            //else
            //{
            //    result.score = null;
            //}

            //if (form.Scores.Count > 0)
            //{
            //    if (form.Scores.Count(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null) != 0)
            //    {
            //        result.finalScore = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null).finalResult;

            //    }
            //}
            //else
            //{
            //    result.score = null;
            //}
            //result.scoreMax = form.finalScoreMax;
            //result.scoreMin = form.finalScoreMin;
            //if (form.Scores.Count > 0)
            //{
            //    if (form.Scores.Count(o => o.candidateId == candidatId && o.formId == formId) != 0)
            //    {
            //        result.total = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId).finalResult;
            //    }
            //}
            //else
            //{
            //    result.total = null;
            //}

            //foreach (var compet in form.Competences)
            //{
            //    result.competencesList.Add(compet.ToDto());
            //    foreach (var dim in compet.Dimensions)
            //    {
            //        //    result.competencesList.Last().dimensions.Add(dim.ToDto());

            //        if (dim.Scores.Count > 0)
            //        {
            //            if (dim.Scores.Count(o => o.candidateId == candidatId && o.formId == formId && o.dimensionId == dim.ID) != 0)
            //            {
            //                result.competencesList.Last().dimensions.Last().score = dim.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.dimensionId == dim.ID).result;
            //            }
            //        }
            //        else
            //        {
            //            result.competencesList.Last().dimensions.Last().notObserved = true;
            //        }
            //    }
            //}
            //foreach (var candi in form.Candidates)
            //{

            //    result.candidateList.Add(candi.ToDto());
            //}

            //return Ok(result);
        }



        //POST: 
        [Route("forms")]
        [ResponseType(typeof(Form))]
        public IHttpActionResult Post([FromBody] DtoForm data)
        {
            Form formDb = new Form();
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            formDb = data.ToEntity(false);

            //competence region
            #region competences
            foreach (var comp in data.competencesList)
            {
                Competence compDb = db.Competences.Find(comp.competenceId);


                if (compDb == null)
                {
                    compDb = new Competence();

                    compDb = comp.ToEntity(false);

                }
                

                //dimension region
                #region dimension
                foreach (var dim in comp.dimensions)
                {
                    Dimension dimDb = db.Dimensions.Find(dim.dimensionId);

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
            if (data.juryList != null)
                foreach (var jury in data.juryList)
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
            if (data.candidateList == null)
                data.candidateList = new List<DtoCandidate>();
            foreach (var candi in data.candidateList)
            {
                Candidate canDb = db.Candidates.Find(candi.candidateId);
                
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
                        JCF.candidate_ID = candi.candidateId;
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

            foreach (var candi in data.candidateList)
            {
                Candidate canDb = db.Candidates.Find(candi.candidateId);

                if (candi.juries != null)
                    foreach (var jur in candi.juries)
                    {
                        juryCandidateForm JCF = new juryCandidateForm();
                        JCF.candidate_ID = candi.candidateId;
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
        
        //[Route("forms/{id}")]
        //[ResponseType(typeof(Form))]
        //public IHttpActionResult Put(int id, DtoForm formNew)
        //{
        //    Form formDb = db.Forms.Find(formNew.formId);
        //    formDb = formNew.ToEntity();
        //    if (id != formNew.formId)
        //    {
        //        return BadRequest();
        //    }
        //    if (formDb == null)        
        //    {
                
        //        return Post(formNew);
        //    }
        //    else
        //    {

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        try
        //        {
        //            db.Entry(formNew.ToEntity()).State = EntityState.Modified;
        //        }
        //        catch (Exception e)
        //        {

        //            Console.WriteLine(e.Message);
        //            return StatusCode(HttpStatusCode.NotModified);
        //        }
              

        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {               
        //            return StatusCode(HttpStatusCode.NotModified);
        //        }

        //        return StatusCode(HttpStatusCode.NoContent);
        //    }

        //}



        [Route("forms/{id}")]
        public IHttpActionResult GetForm(int id)
        {
            DtoForm form = db.Forms.Find(id).ToDto();
            if (form == null)
            {
                return NotFound();
            }
            if (form.candidate != null)
            {
                if (form.candidate.juries == null)
                {
                    form.candidate.juries = new List<DtoJury>();
                }

            }
            foreach (var c in form.candidateList)
            {
                if (c.juries == null)
                {
                    c.juries = new List<DtoJury>();
                }
                foreach (var item in db.JuryCandidateForms.Where(o => o.candidate_ID == c.candidateId && o.form_ID == id))
                {
                    foreach (var j in db.Juries.Where(j => j.ID == item.jury_ID).ToList())
                    {

                        c.juries.Add(j.ToDto());
                    }
                }
            }
            return Ok(form);
        }


        // PUT: api/Forms/5

        [Route("forms/{id}")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutForm(int id, [FromBody]  DtoForm content)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (content == null)
            {
                return BadRequest();
            }
            if (id != content.formId)
            {
                return BadRequest();
            }
            Form dbform = db.Forms.Where(f => f.ID == id).First();

            dbform.Juries = content.ToEntity().Juries;
            dbform.MaxCandidates = content.ToEntity().MaxCandidates;
            dbform.name = content.ToEntity().name;
            dbform.name_DE = content.ToEntity().name_DE;
            dbform.name_EN = content.ToEntity().name_EN;
            dbform.name_FR = content.ToEntity().name_FR;
            dbform.name_NL = content.ToEntity().name_NL;
            dbform.description_DE = content.ToEntity().description_DE;
            dbform.description_EN = content.ToEntity().description_EN;
            dbform.description_FR = content.ToEntity().description_FR;
            dbform.description_NL = content.ToEntity().description_NL;
            dbform.finalScoreMax = content.ToEntity().finalScoreMax;
            dbform.finalScoreMin = content.ToEntity().finalScoreMin;
            dbform.code = content.ToEntity().code;

            foreach (var cand in dbform.Candidates)
            {
                var dtoCandidate = content.candidateList.Where(c => c.candidateId == cand.ID).First();

                //Remove candidate if not in dto form
                if (!content.candidateList.Any(c => c.candidateId == cand.ID))
                {
                    dbform.Candidates.Remove(cand);
                    foreach (var item in db.JuryCandidateForms.Where(o => o.candidate_ID == cand.ID && o.form_ID == id))
                    {
                        db.JuryCandidateForms.Remove(item);
                    }
                }
                //Candidate in dto form
                else
                {

                    //Assign properties
                    cand.lastname = dtoCandidate.lastName;
                    cand.firstname = dtoCandidate.firstName;
                    cand.nationalNumber = dtoCandidate.nationalNumber;


                    foreach (var dtocand in content.candidateList)
                    {
                        if (!dbform.Candidates.Any(c => c.ID == dtocand.candidateId))
                        {
                            dbform.Candidates.Add(dtocand.ToEntity());
                        }
                    }


                    //remove in db if removed from dto
                    foreach (var item in db.JuryCandidateForms.Where(o => o.candidate_ID == cand.ID && o.form_ID == id))
                    {
                        if (!dtoCandidate.juries.Any(j=> j.juryId==item.jury_ID))
                        {
                            db.JuryCandidateForms.Remove(item);
                        }
                    }
                    

                    //add to db if present in dto
                    foreach(var assignedJury in dtoCandidate.juries)
                    {                       
                        if(!db.JuryCandidateForms.Any(jcf=> jcf.candidate_ID == cand.ID && jcf.form_ID== id && jcf.jury_ID== assignedJury.juryId))
                        {

                            db.JuryCandidateForms.Add(new juryCandidateForm
                            {
                                candidate_ID = cand.ID,
                                form_ID = id,
                                jury_ID = assignedJury.juryId

                            });
                            
                        }

                    }
                }
            }



            dbform.Scores = content.ToEntity().Scores;
            dbform.Competences = content.ToEntity().Competences;




            db.Entry(dbform).State = EntityState.Modified;

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

        






        // GET: api/Forms
        public IQueryable<Form> GetForms()
        {
            return db.Forms;
        }

        //// GET: api/Forms/5
        //[ResponseType(typeof(Form))]
        //public IHttpActionResult GetForm(int id)
        //{
        //    Form form = db.Forms.Find(id);
        //    if (form == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(form);
        //}

        //// PUT: api/Forms/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutForm(int id, Form form)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //        if (id != form.ID)
        //        {
        //            return BadRequest();
        //}

        //    db.Entry(form).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FormExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

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

        [Route("forms/{id}")]
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

        [Route("forms/")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("forms/{id}")]
        private bool FormExists(int id)
        {
            return db.Forms.Count(e => e.ID == id) > 0;
        }
    }
}