using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models
{
    public static class DtoExtension
    {

        #region Dimension
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

            result.ID = source.dimensionID;
            return result;
        }

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

            result.dimensionID = source.ID;

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
        public static Competence ToEntity(this DtoCompetence source)
        {
            Competence result = new Competence();

            result.ID = source.competenceID;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

            result.description_DE = source.description_DE;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;
            result.description_EN = source.description_EN;

            result.status = source.status;
            result.weight = source.weight;
            result.code = source.code;
            result.comment = source.comment;


            return result;
        }

        public static DtoCompetence ToDto(this Competence source, String language = "EN")
        {

            DtoCompetence result = new DtoCompetence();
            result.competenceID = source.ID;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

            result.description_DE = source.description_DE;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;
            result.description_EN = source.description_EN;

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

            if (result.dimensions == null)
            {
                result.dimensions = new List<DtoDimension>();
            }
            foreach (var dim in source.Dimensions)
            {
                result.dimensions.Add(dim.ToDto());
            }

            return result;
        }
        #endregion



        #region Jury
        public static Jury ToEntity(this DtoJury source)
        {
            Jury result = new Jury();
            result.ID = source.juryId;
            result.lastname = source.lastname;
            result.firstname = source.firstname;


            return result;
        }

        public static DtoJury ToDto(this Jury source)
        {
            DtoJury result = new DtoJury();
            result.juryId = source.ID;
            result.lastname = source.lastname;
            result.firstname = source.firstname;



            return result;
        }
        #endregion



        #region Candidate
        public static Candidate ToEntity(this DtoCandidate source)
        {
            Candidate result = new Candidate();
            result.ID = source.candidateID;
            result.firstname = source.firstname;
            result.lastname = source.lastname;
            result.nationalNumber = source.nationalNumber;


            return result;
        }

        public static DtoCandidate ToDto(this Candidate source)
        {
            DtoCandidate result = new DtoCandidate();
            result.candidateID = source.ID;
            result.firstname = source.firstname;
            result.lastname = source.lastname;
            result.nationalNumber = source.nationalNumber;
            result.name = result.lastname + " " + result.firstname;


            return result;
        }
        #endregion



        #region Form
        public static Form ToEntity(this DtoForm source)
        {
            Form result = new Form();

            result.ID = source.formID;
            result.code = source.code;
            result.description_DE = source.description_DE;
            result.description_EN = source.description_EN;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

            

            if (result.Candidates == null)
            {
                result.Candidates = new List<Candidate>();
            }
            if (source.candidateList != null)
                foreach (var item in source.candidateList)
                {

                    result.Candidates.Add(item.ToEntity());
                }

            if (result.Competences == null)
            {
                result.Competences = new List<Competence>();
            }
            if (source.competencesList != null)
                foreach (var item in source.competencesList)
                {

                    result.Competences.Add(item.ToEntity());
                }

            result.finalScoreMax = source.finalScoreMax;
            result.finalScoreMin = source.finalScoreMin;
            result.MaxCandidates = source.maxCandidates;

            if (result.Juries == null)
            {
                result.Juries = new List<Jury>();
            }
            if (source.juryList != null)
                foreach (var item in source.juryList)
                {
                    result.Juries.Add(item.ToEntity());
                }

            return result;
        }

        public static DtoForm ToDto(this Form source, String language = "EN")
        {
            DtoForm result = new DtoForm();

            result.formID = source.ID;
            result.code = source.code;
            result.description_DE = source.description_DE;
            result.description_EN = source.description_EN;
            result.description_FR = source.description_FR;
            result.description_NL = source.description_NL;

            result.name_DE = source.name_DE;
            result.name_FR = source.name_FR;
            result.name_NL = source.name_NL;
            result.name_EN = source.name_EN;

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

            if (result.candidateList==null)
            {
                result.candidateList = new List<DtoCandidate>();
            }
            if (source.Candidates != null)
                foreach (var item in source.Candidates)
                {

                    result.candidateList.Add(item.ToDto());
                }
            
            if (result.competencesList == null)
            {
                result.competencesList = new List<DtoCompetence>();
            }
            if (source.Competences != null)
                foreach (var item in source.Competences)
                {

                    result.competencesList.Add(item.ToDto());
                }

            result.finalScoreMax = source.finalScoreMax;
            result.finalScoreMin = source.finalScoreMin;
            result.maxCandidates = source.MaxCandidates;

            if (result.juryList == null)
            {
                result.juryList = new List<DtoJury>();
            }
            if (source.Juries != null)
                foreach (var item in source.Juries)
                {
                    result.juryList.Add(item.ToDto());
                }
            
            return result;
        }

        #endregion


        #region Screening
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

            result.ID = source.ID;

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

            result.ID = source.ID;

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

            foreach (var judge in source.Judges)
            {
                result.jugesList.Add(judge.ToDto());
            }

            foreach (var candi in source.Candidates)
            {
                result.candidateList.Add(candi.ToDto());
            }

            return result;
        }

        #endregion




    }
}