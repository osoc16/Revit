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
        /// <summary>
        /// the DataContext used by the controler
        /// </summary>
        private DataContext db = new DataContext();



        /// <summary>
        /// GET: api/juries/{juryId}/forms/{formId}/candidates/{candidatId}
        /// 
        /// Used by the frontend to recieve an evaluation form for a particuliar candidate and a particular jury member
        /// </summary>
        /// <param name="juryId"></param>
        /// <param name="formId"></param>
        /// <param name="candidatId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
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
                    if (!result.candidate.juries.Contains(j.ToDto()))
                    {

                        result.candidate.juries.Add(j.ToDto());
                    }
                }
            }
            
            if (form.Scores.Count>0)
            {
                if (form.Scores.Any(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null && o.juryId == juryId))
                {
                    result.score = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null && o.juryId == juryId).result;
                    result.total = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null && o.juryId == juryId).finalResult;
                }

            }
            else
            {
                result.score = null;
            }

            if (form.Scores.Count > 0)
            {
                if (form.Scores.Any(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null && o.juryId == juryId) )
                {
                    result.finalScore = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == null && o.dimensionId == null && o.juryId == juryId).finalResult;

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
                if (form.Scores.Any(o => o.candidateId == candidatId && o.formId == formId && o.juryId == juryId) )
                {
                    result.total = form.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.juryId == juryId).finalResult;
                }
            }
            else
            {
                result.total = null;
            }

            foreach (var compet in form.Competences)
            {
                result.competencesList.Add(compet.ToDto(language));
                if (compet.Scores.Any(o => o.candidateId == candidatId && o.formId == formId && o.competenceId == compet.ID && o.juryId == juryId))
                {

                    result.competencesList.Last().score = compet.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.dimensionId == null && o.competenceId == compet.ID && o.juryId == juryId).result;
                }
                foreach (var dim in compet.Dimensions)
                {

                    if (dim.Scores.Count > 0)
                    {
                        if (dim.Scores.Any(o => o.candidateId == candidatId && o.formId == formId && o.dimensionId == dim.ID && o.juryId == juryId))
                        {
                            result.competencesList.Last().dimensions.Where(di => di.dimensionId == dim.ID).First().score = dim.Scores.First(o => o.candidateId == candidatId && o.formId == formId && o.dimensionId == dim.ID && o.juryId == juryId).result;
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
                        if (!c.juries.Contains(j.ToDto()))
                        {
                            c.juries.Add(j.ToDto());
                        }
                    }
                }
            }

            return Ok(result);
        }



        /// <summary>
        /// used by the frontend to recieve an evaluation form to modify it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("forms/{id}")]
        //GET api/Forms/5
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
                        if (c.juries.Contains(j.ToDto()))
                        {

                        c.juries.Add(j.ToDto());
                        }
                    }
                }
            }
            return Ok(form);
        }


        /// <summary>
        /// Used by the frontend to send an evaluation form for a particuliar candidate and a particular jury member
        /// </summary>
        /// <param name="juryId"></param>
        /// <param name="formId"></param>
        /// <param name="candidatId"></param>
        /// <param name="data"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        [Route("evaluations/juries/{juryId}/forms/{formId}/candidates/{candidatId}")]
        [ResponseType(typeof(DtoForm))]
        // PUT: api/juries/{juryId}/forms/{formId}/candidates/{candidatId}
        public object Put(int juryId, int formId, int candidatId, [FromBody] DtoForm data, string language = "en")
        {

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

            Form dbForm = db.Forms.Find(formId);
            if (dbForm == null)
            {
                return NotFound();
            }
            Candidate dbCandidate = db.Candidates.Find(candidatId);
            if (dbCandidate == null)
            {
                return NotFound();
            }
            

                if (dbForm.Scores.Any(s => s.candidateId == candidatId && s.formId== dbForm.ID && s.dimensionId==null && s.competenceId==null && s.juryId==juryId))
                {
                    dbForm.Scores.Where(s => s.candidateId == candidatId && s.formId == dbForm.ID && s.dimensionId == null && s.competenceId == null && s.juryId == juryId).First().result = data.score;
                    dbForm.Scores.Where(s => s.candidateId == candidatId && s.formId == dbForm.ID && s.dimensionId == null && s.competenceId == null && s.juryId == juryId).First().finalResult = data.finalScore;
                }
                else
                {
                    dbForm.Scores.Add(new Score
                    {
                        candidateId = dbCandidate.ID,
                        formId = dbForm.ID,
                        result = data.score,
                        finalResult=data.finalScore,
                        juryId = juryId
                    });
                }


            foreach (var dtoComp in data.competencesList)
            {
                var dbComp = dbForm.Competences.Where(c => c.ID == dtoComp.competenceId).First();

                if (dbForm.Scores.Any(s => s.candidateId == candidatId && s.formId == dbForm.ID && s.dimensionId == null && s.competenceId == dtoComp.competenceId && s.juryId == juryId))
                {
                    dbForm.Scores.Where(s => s.candidateId == candidatId && s.formId == dbForm.ID && s.dimensionId == null && s.competenceId == dtoComp.competenceId && s.juryId == juryId).First().result = dtoComp.score;
                    dbForm.Scores.Where(s => s.candidateId == candidatId && s.formId == dbForm.ID && s.dimensionId == null && s.competenceId == dtoComp.competenceId && s.juryId == juryId).First().finalResult = dtoComp.score;
                }
                else
                {
                    dbComp.Scores.Add(new Score
                    {
                        
                        candidateId = dbCandidate.ID,
                        formId = dbForm.ID,
                        result = dtoComp.score,
                        finalResult = dtoComp.finalScore,
                        competenceId = dbComp.ID,
                        juryId = juryId
                    });
                }


                foreach (var dtoDim in dtoComp.dimensions)
                {
                        var dbDim = dbComp.Dimensions.Where(c => c.ID == dtoDim.dimensionId).First();

                        if (dbForm.Scores.Any(s => s.candidateId == candidatId && s.formId == dbForm.ID && s.dimensionId == dbDim.ID && s.competenceId == dtoComp.competenceId && s.juryId == juryId))
                        {
                            dbForm.Scores.Where(s => s.candidateId == candidatId && s.dimensionId == dbDim.ID && s.juryId == juryId).First().result = dtoDim.score;
                        }
                    else
                    {
                        dbDim.Scores.Add(new Score
                        {
                            dimensionId = dbDim.ID,
                            candidateId = dbCandidate.ID,
                            formId = dbForm.ID,
                            result = dtoComp.score,
                            competenceId = dbComp.ID,
                            juryId = juryId
                        });
                    }

                }
        }


            db.Entry(dbForm).State = EntityState.Modified;
            

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


///////////////////////////////////////            
        }


        /// <summary>
        /// Used by the frontend to send an evaluation form modified or create a new one
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [Route("forms/{id}")]
        [ResponseType(typeof(void))]
        [HttpPut]
        // PUT: api/Forms/5
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
            Form dbForm = db.Forms.Where(f => f.ID == id).First();

            
            dbForm.MaxCandidates = content.ToEntity().MaxCandidates;
            dbForm.name = content.ToEntity().name;
            dbForm.name_DE = content.ToEntity().name_DE;
            dbForm.name_EN = content.ToEntity().name_EN;
            dbForm.name_FR = content.ToEntity().name_FR;
            dbForm.name_NL = content.ToEntity().name_NL;
            dbForm.description_DE = content.ToEntity().description_DE;
            dbForm.description_EN = content.ToEntity().description_EN;
            dbForm.description_FR = content.ToEntity().description_FR;
            dbForm.description_NL = content.ToEntity().description_NL;
            dbForm.finalScoreMax = content.ToEntity().finalScoreMax;
            dbForm.finalScoreMin = content.ToEntity().finalScoreMin;
            dbForm.code = content.ToEntity().code;


            foreach (var dtoCandi in content.candidateList)
            {
                if (dtoCandi.candidateId == 0)
                {
                    dbForm.Candidates.Add(new Candidate { firstname = dtoCandi.firstName, lastname = dtoCandi.lastName});
                    
                }
            }
            db.Entry(dbForm).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                db = new DataContext();
                dbForm = db.Forms.Where(f => f.ID == id).First();
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

            //db 
            var dbListjury = db.JuryCandidateForms.Where(o => o.form_ID == id);
            var dbListJCF = db.JuryCandidateForms.Where(o => o.form_ID == id);
            //db



            var listJugeToDel = new List<Jury>();
            //dbForm.Juries = content.ToEntity().Juries;
            foreach (var juge in dbForm.Juries)
            {
                if (!content.juryList.Any(j=> j.juryId == juge.ID))
                {
                    listJugeToDel.Add(juge);
                }
            }
            foreach (var item in listJugeToDel)
            {
                dbForm.Juries.Remove(item);
            }
            listJugeToDel.Clear();


            var listCandToAdd = new List<Candidate>();
            var listCandToDel = new List<Candidate>();
            foreach (var cand in dbForm.Candidates)
            {
                if (!content.candidateList.Any(c=> c.candidateId== cand.ID))
                {
                    listCandToDel.Add(cand);
                }
               
            }
            foreach (var item in listCandToDel)
            {
                dbForm.Candidates.Remove(item);
            }
            listCandToDel.Clear();

            foreach (var dtocand in content.candidateList)
            {
                if (!dbForm.Candidates.Any(c => c.ID == dtocand.candidateId))
                {
                    listCandToAdd.Add(dtocand.ToEntity());
                }
            }

            foreach (var cand in dbForm.Candidates)
            {

                var dtoCandidate = content.candidateList.Where(c => c.candidateId == cand.ID).First();

                //Remove candidate if not in dto form
                if (!content.candidateList.Any(c => c.candidateId == cand.ID))
                {
                    listCandToDel.Add(cand);
                    foreach (var item in dbListjury.Where(o => o.candidate_ID == cand.ID))
                    {
                        db.JuryCandidateForms.Remove(item);
                    }
                }
                

            }

            foreach (var item in listCandToDel)
            {
                dbForm.Candidates.Remove(item);
            }
            listCandToDel.Clear();
            foreach (var item in listCandToAdd)
            {
                dbForm.Candidates.Add(item);
            }
            listCandToAdd.Clear();

            db.Entry(dbForm).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                db = new DataContext();
                dbForm = db.Forms.Where(f => f.ID == id).First();
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
            foreach (var item in content.candidateList)
            {
                if (item.candidateId ==0)
                {
                    item.candidateId = dbForm.Candidates.Where(c => c.nationalNumber == item.nationalNumber && c.lastname == item.lastName && c.firstname == item.firstName).Last().ID;
                }
            }

            //Candidate in dto form
            foreach (var cand in dbForm.Candidates)
            {
                var dtoCandidate = content.candidateList.Where(c => c.candidateId == cand.ID).First();

                //Assign properties
                cand.lastname = dtoCandidate.lastName;
                    cand.firstname = dtoCandidate.firstName;
                    cand.nationalNumber = dtoCandidate.nationalNumber;





                    //remove in db if removed from dto
                    foreach (var item in dbListjury.Where(o => o.candidate_ID == cand.ID))
                    {
                        if (!dtoCandidate.juries.Any(j => j.juryId == item.jury_ID))
                        {

                        //db.JuryCandidateForms.Find(item);
                        db.JuryCandidateForms.Attach(item);
                        db.JuryCandidateForms.Remove(item);
                        }
                    }




                    //add to db if present in dto
                    foreach (var assignedJury in dtoCandidate.juries)
                    {

                        if (!db.JuryCandidateForms.Where(o => o.form_ID == id).Any(jcf => jcf.candidate_ID == cand.ID && jcf.jury_ID == assignedJury.juryId))
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


            var listCompToDel = new List<Competence>();
            var listCompToAdd = new List<Competence>();

            foreach (var dbComp  in dbForm.Competences)
            {
                if (content.ToEntity().Competences.Any(c => c.ID == dbComp.ID))
                {
                    var compet = content.ToEntity().Competences.Where(c => c.ID == dbComp.ID).First();
                    dbComp.code = compet.code;
                    dbComp.comment = compet.comment;
                    dbComp.description_DE = compet.description_DE;
                    dbComp.description_EN = content.competencesList.Where(c => c.competenceId == dbComp.ID).First().description;
                    dbComp.description_FR = compet.description_FR;
                    dbComp.description_NL = compet.description_NL;
                    dbComp.name_DE = compet.name_DE;
                   // dbComp.name_EN = compet.name_EN;
                    dbComp.name_EN = content.competencesList.Where(c => c.competenceId == dbComp.ID).First().name;
                    dbComp.name_FR = compet.name_FR;
                    dbComp.name_NL = compet.name_NL;
                    dbComp.weight = compet.weight;
                    dbComp.status = compet.status;
                    dbComp.statusMessage_DE = compet.statusMessage_DE;
                    dbComp.statusMessage_EN = compet.statusMessage_EN;
                    dbComp.statusMessage_FR = compet.statusMessage_FR;
                    dbComp.statusMessage_NL = compet.statusMessage_NL;

                    var listDimToDel = new List<Dimension>();
                    foreach (var dbDim in dbComp.Dimensions)
                    {

                        if (compet.Dimensions.Any(d => d.ID == dbDim.ID))
                        {
                            var diment = compet.Dimensions.Where(d => d.ID == dbDim.ID).First();
                            dbDim.code = diment.code;
                            dbDim.description_DE = diment.description_DE;
                           // dbDim.description_EN = diment.description_EN;
                            dbDim.description_EN = content.competencesList.Where(c => c.competenceId == dbComp.ID).First().dimensions.Where(d => d.dimensionId == dbDim.ID).First().description;
                            dbDim.description_FR = diment.description_FR;
                            dbDim.description_NL = diment.description_NL;
                            dbDim.name_DE = diment.name_DE;
                            //dbDim.name_EN = diment.name_EN;
                            dbDim.name_EN = content.competencesList.Where(c => c.competenceId == dbComp.ID).First().dimensions.Where(d => d.dimensionId == dbDim.ID).First().name;
                            dbDim.name_FR = diment.name_FR;
                            dbDim.name_NL = diment.name_NL;

                        }
                        else
                        {
                            listDimToDel.Add(dbDim);
                        }
                    }

                    foreach (var item in listDimToDel)
                    {
                        dbComp.Dimensions.Remove(item);
                    }

                    listCompToDel.Clear();
                    foreach (var dtodim in compet.Dimensions)
                    {
                        if (!dbComp.Dimensions.Any(d => d.ID == dtodim.ID))
                        {
                            dbComp.Dimensions.Add(dtodim);
                        }
                    }
                }
                else
                {
                    listCompToDel.Add(dbComp);
                }
            }

            foreach (var dtoComp in content.ToEntity().Competences)
            {
                if (!dbForm.Competences.Any(d => d.ID == dtoComp.ID))
                {
                    listCompToAdd.Add(dtoComp);
                }
            }

            
            foreach (var item in listCompToDel)
            {
                dbForm.Competences.Remove(item);
            }
            listCompToDel.Clear();


            foreach (var item in listCompToAdd)
            {
                dbForm.Competences.Add(item);
            }
            listCompToAdd.Clear();


            db.Entry(dbForm).State = EntityState.Modified;

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



        /// <summary>
        /// used by the PUT function
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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

 



        








        // GET: api/Forms
        public IQueryable<Form> GetForms()
        {
            return db.Forms;
        }

        

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