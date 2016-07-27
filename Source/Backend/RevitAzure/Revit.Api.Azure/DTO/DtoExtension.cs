using Revit.Api.Azure.DTO;
using Revit.Api.Azure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.DTO
{
    public static class DtoExtension
    {

        #region Dimension
        /// <summary>
        /// translate a DTO recieve from the frontend to item usable by the database
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Dimension ToEntity(this DtoDimension source)
        {
            Dimension result = new Dimension();
            result.code = source.code;
            result.description_DE = source.description_DE;
            result.description_EN = source.description_EN;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

            if (source.name_EN == null && source.name != null)
            {
                result.name_EN = source.name;
            }
            if (source.description_EN == null && source.description != null)
            {
                result.description_EN = source.description;
            }
            result.ID = source.dimensionId;
            return result;
        }
        /// <summary>
        /// translate a database object to item usable by the frontend
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DtoDimension ToDto(this Dimension source,String language = "EN")
        {
            DtoDimension result = new DtoDimension();
            result.code = source.code;
            result.description_DE = source.description_DE;
            result.description_EN = source.description_EN;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

            if (result.scoresList ==null)
            {
                result.scoresList = new List<DtoScore>();
            }
            foreach (var item in source.Scores)
            {
                result.scoresList.Add(item.ToDto());
            }
            result.dimensionId = source.ID;

            switch (language)
            {
                case "en":
                    {
                        result.name = source.name_EN;
                        result.description = source.description_EN;
                        break;
                    }
                case "fr":
                    {
                        result.name = source.name_FR;
                        result.description = source.description_FR;
                        break;
                    }
                case "nl":
                    {
                        result.name = source.name_NL;
                        result.description = source.description_NL;
                        break;
                    }
                case "de":
                    {
                        result.name = source.name_DE;
                        result.description = source.description_DE;
                        break;
                    }
                default:
                    {
                        result.name = source.name_EN;
                        result.description = source.description_EN;
                        break;
                    }
            }
            return result;
        }

        #endregion



        #region Competence

        /// <summary>
        /// translate a DTO recieve from the frontend to item usable by the database
        /// </summary>
        /// <param name="source"></param>
        /// <param name="chain">allow to choise if the functon translate all the sub item or not</param>
        /// <returns></returns>
        public static Competence ToEntity(this DtoCompetence source, bool chain = true)
        {
            Competence result = new Competence();

            result.ID = source.competenceId;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

            result.description_DE = source.description_DE;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;
            result.description_EN = source.description_EN;
            result.statusMessage_DE = source.statusMessage_DE;
            result.statusMessage_FR = source.statusMessage_FR;
            result.statusMessage_NL = source.statusMessage_NL;
            result.statusMessage_EN = source.statusMessage_EN;

            result.status = source.status;
            result.weight = source.weight;
            result.code = source.code;
            result.comment = source.comment;

            if (source.name_EN==null && source.name!=null)
            {
                result.name_EN = source.name;
            }
            if (source.description_EN == null && source.description != null)
            {
                result.description_EN = source.description;
            }

            if (result.Dimensions == null)
            {
                result.Dimensions = new List<Dimension>();
            }
            if (chain == true)
            {
                foreach (var dim in source.dimensions)
                {
                    result.Dimensions.Add(dim.ToEntity());
                }
            }
            return result;
        }

        /// <summary>
        /// translate a database object to item usable by the frontend
        /// </summary>
        /// <param name="source"></param>
        /// <param name="chain">allow to choise if the functon translate all the sub item or not</param>
        /// <param name="language">parameter for language</param>
        /// <returns></returns>
        public static DtoCompetence ToDto(this Competence source, String language = "EN", bool chain = true)
        {

            DtoCompetence result = new DtoCompetence();
            result.competenceId = source.ID;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

            result.description_DE = source.description_DE;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;
            result.description_EN = source.description_EN;

            result.statusMessage_DE = source.statusMessage_DE;
            result.statusMessage_FR = source.statusMessage_FR;
            result.statusMessage_NL = source.statusMessage_NL;
            result.statusMessage_EN = source.statusMessage_EN;

            result.status = source.status;
            result.weight = source.weight;
            result.code = source.code;
            result.comment = source.comment;

            switch (language)
            {
                case "en":
                    {
                        result.name = source.name_EN;
                        result.description = source.description_EN;

                        result.statusMessage = source.statusMessage_EN;
                        break;
                    }
                case "fr":
                    {
                        result.name = source.name_FR;
                        result.description = source.description_FR;
                        result.statusMessage = source.statusMessage_FR;
                        break;
                    }
                case "nl":
                    {
                        result.name = source.name_NL;
                        result.description = source.description_NL;
                        result.statusMessage = source.statusMessage_NL;
                        break;
                    }
                case "de":
                    {
                        result.name = source.name_DE;
                        result.description = source.description_DE;
                        result.statusMessage = source.statusMessage_DE;
                        break;
                    }
                default:
                    {
                        result.name = source.name_EN;
                        result.description = source.description_EN;
                        result.statusMessage = source.statusMessage_EN;
                        break;
                    }
            }

            if (result.dimensions == null)
            {
                result.dimensions = new List<DtoDimension>();
            }
            if (chain == true)
                foreach (var dim in source.Dimensions)
                {
                    result.dimensions.Add(dim.ToDto());
                }

            return result;
        }
        #endregion



        #region Jury

        /// <summary>
        /// translate a DTO recieve from the frontend to item usable by the database
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Jury ToEntity(this DtoJury source)
        {
            Jury result = new Jury();
            result.ID = source.juryId;
            result.lastname = source.lastName;
            result.firstname = source.firstName;


            return result;
        }

        /// <summary>
        /// translate a database object to item usable by the frontend
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DtoJury ToDto(this Jury source)
        {
            DtoJury result = new DtoJury();
            result.juryId = source.ID;
            result.lastName = source.lastname;
            result.firstName = source.firstname;



            return result;
        }
        #endregion



        #region Candidate

        /// <summary>
        /// translate a DTO recieve from the frontend to item usable by the database
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Candidate ToEntity(this DtoCandidate source)
        {
            Candidate result = new Candidate();
            result.ID = source.candidateId;
            result.firstname = source.firstName;
            result.lastname = source.lastName;
            result.nationalNumber = source.nationalNumber;


            return result;
        }

        /// <summary>
        /// translate a database object to item usable by the frontend
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DtoCandidate ToDto(this Candidate source)
        {
            DtoCandidate result = new DtoCandidate();
            result.candidateId = source.ID;
            result.firstName = source.firstname;
            result.lastName = source.lastname;
            result.nationalNumber = source.nationalNumber;
            result.name = result.lastName + " " + result.firstName;


            return result;
        }
        #endregion



        #region Form       

        /// <summary>
        /// translate a DTO recieve from the frontend to item usable by the database
        /// </summary>
        /// <param name="source"></param>
        /// <param name="chain">allow to choise if the functon translate all the sub item or not </param>
        /// <returns></returns>
        public static Form ToEntity(this DtoForm source, bool chain = true)
        {
            Form result = new Form();

            result.ID = source.formId;
            result.code = source.code;
            result.description_DE = source.description_DE;
            result.description_EN = source.description_EN;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;

            result.name = source.name;
            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

            

            if (result.Candidates == null)
            {
                result.Candidates = new List<Candidate>();
            }
            if (result.Competences == null)
            {
                result.Competences = new List<Competence>();
            }
            if (result.Juries == null)
            {
                result.Juries = new List<Jury>();
            }
            if (chain == true)
            {
                if (source.candidateList != null)
                    foreach (var item in source.candidateList)
                    {
                        result.Candidates.Add(item.ToEntity());
                    }

            
                if (source.competencesList != null)
                    foreach (var item in source.competencesList)
                    {

                        result.Competences.Add(item.ToEntity());
                    }

            
                if (source.juryList != null)
                    foreach (var item in source.juryList)
                    {
                        result.Juries.Add(item.ToEntity());
                    }
            }
            

            result.finalScoreMax = source.finalScoreMax;
            result.finalScoreMin = source.finalScoreMin;
            result.MaxCandidates = source.maxCandidates;

            return result;
        }




        /// <summary>
        /// translate a database object to item usable by the frontend
        /// </summary>
        /// <param name="source"></param>
        /// <param name="chain">allow to choise if the functon translate all the sub item or not</param>
        /// <param name="language">parameter for language</param>
        /// <returns></returns>
        public static DtoForm ToDto(this Form source, bool chain = true, String language = "EN")
        {
            DtoForm result = new DtoForm();

            result.formId = source.ID;
            result.code = source.code;
            result.description_DE = source.description_DE;
            result.description_EN = source.description_EN;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;

            result.name = source.name;
            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

            switch (language)
            {
                case "en":
                    {
                        result.description = source.description_EN;
                        break;
                    }
                case "fr":
                    {
                        result.description = source.description_FR;
                        break;
                    }
                case "nl":
                    {
                        result.description = source.description_NL;
                        break;
                    }
                case "de":
                    {
                        result.description = source.description_DE;
                        break;
                    }
                default:
                    {
                        result.description = source.description_EN;
                        break;
                    }
            }

            if (result.juryList == null)
            {
                result.juryList = new List<DtoJury>();
            }
            if (result.candidateList==null)
            {
                result.candidateList = new List<DtoCandidate>();
            }
            if (result.competencesList == null)
            {
                result.competencesList = new List<DtoCompetence>();
            }

            if ( chain == true)
            {
                if (source.Candidates != null)
                foreach (var item in source.Candidates)
                {
                    result.candidateList.Add(item.ToDto());
                }
            if (source.Competences != null)
                foreach (var item in source.Competences)
                {
                    result.competencesList.Add(item.ToDto());
                }
            if (source.Juries != null)
                foreach (var item in source.Juries)
                {
                    result.juryList.Add(item.ToDto());
                }
            }

            result.finalScoreMax = source.finalScoreMax;
            result.finalScoreMin = source.finalScoreMin;
            result.maxCandidates = source.MaxCandidates;

            return result;
        }

        #endregion



        #region Screening

        /// <summary>
        /// translate a DTO recieve from the frontend to item usable by the database
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Screening ToEntity(this DtoScreening source)
        {
            Screening result = new Screening();
            result.code = source.code;
            result.description_DE = source.description_DE;
            result.description_EN = source.description_EN;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;
            result.Form = source.form.ToEntity();

            result.ID = source.screeningId;

            foreach (var judge in source.jugesList)
            {
                result.Judges.Add(judge.ToEntity());
            }

            foreach (var candi in source.candidateList)
            {
                result.Candidates.Add(candi.ToEntity());
            }

            return result;
        }

        /// <summary>
        /// translate a database object to item usable by the frontend
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DtoScreening ToDto(this Screening source, String language = "EN")
        {
            DtoScreening result = new DtoScreening();
            result.code = source.code;
            result.description_DE = source.description_DE;
            result.description_EN = source.description_EN;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;
            result.form = source.Form.ToDto();

            result.screeningId = source.ID;

            switch (language)
            {
                case "en":
                    {
                        result.name = source.name_EN;
                        result.description = source.description_EN;
                        break;
                    }
                case "fr":
                    {
                        result.name = source.name_FR;
                        result.description = source.description_FR;
                        break;
                    }
                case "nl":
                    {
                        result.name = source.name_NL;
                        result.description = source.description_NL;
                        break;
                    }
                case "de":
                    {
                        result.name = source.name_DE;
                        result.description = source.description_DE;
                        break;
                    }
                default:
                    {
                        result.name = source.name_EN;
                        result.description = source.description_EN;
                        break;
                    }
            }
            if (result.jugesList==null)
            {
                result.jugesList = new List<DtoJury>();
            }
            foreach (var judge in source.Judges)
            {
                result.jugesList.Add(judge.ToDto());
            }

            if (result.candidateList == null)
            {
                result.candidateList = new List<DtoCandidate>();
            }
            foreach (var candi in source.Candidates)
            {
                result.candidateList.Add(candi.ToDto());
            }

            return result;
        }

        #endregion



        #region Score

        /// <summary>
        /// translate a DTO recieve from the frontend to item usable by the database
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Score ToEntity(this DtoScore source)
        {
            Score result = new Score();
            result.id = source.scoreId;
            result.formId = source.formId;
            result.juryId = source.juryId;
            result.dimensionId = source.dimensionId;
            result.competenceId = source.competenceId;
            result.candidateId = source.candidateId;
            result.finalResult = source.finalResult;
            result.result = source.result;


            return result;
        }

        /// <summary>
        /// translate a database object to item usable by the frontend
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DtoScore ToDto(this Score source)
        {
            DtoScore result = new DtoScore();
            result.scoreId = source.id;
            result.formId = source.formId;
            result.juryId = source.juryId;
            result.dimensionId = source.dimensionId;
            result.competenceId = source.competenceId;
            result.candidateId = source.candidateId;
            result.finalResult = source.finalResult;
            result.result = source.result;




            return result;
        }
        #endregion

    }
}