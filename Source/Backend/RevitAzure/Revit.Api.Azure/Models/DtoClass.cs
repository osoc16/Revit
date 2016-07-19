﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models
{
    public class DtoDimension
    {
        public int dimensionID { get; set; }

        public string code { get; set; }

        public string name { get; set; }
        public string name_FR { get; set; }
        public string name_EN { get; set; }
        public string name_NL { get; set; }
        public string name_DE { get; set; }

        public string description { get; set; }
        public string description_FR { get; set; }
        public string description_NL { get; set; }
        public string description_EN { get; set; }
        public string description_DE { get; set; }

        public bool notObserved { get; set; }

        public int? score { get; set; }
    }

    public class DtoForm
    {
        public int formID { get; set; }

        public string name { get; set; }
        public string name_FR { get; set; }
        public string name_EN { get; set; }
        public string name_NL { get; set; }
        public string name_DE { get; set; }

        public string description { get; set; }
        public string description_FR { get; set; }
        public string description_NL { get; set; }
        public string description_EN { get; set; }
        public string description_DE { get; set; }

        public int? scoreMin { get; set; }
        public int? scoreMax { get; set; }
        public int? total { get; set; }
        public int? score { get; set; }
        public int? finalScore { get; set; }
        public int? finalScoreMax { get; set; }
        public int? finalScoreMin { get; set; }
        public DtoCandidate candidate { get; set; }
        public ICollection< DtoCompetences> competences { get; set; }
        public ICollection<DtoCandidate> candidateList { get; set; }
    }


    public class DtoCandidate
    {
        public int candidateID { get; set; }

        public string name { get; set; }
    }

    public class DtoCompetences
    {
        public int competenceID { get; set; }
        public string name { get; set; }
        public string name_FR { get; set; }
        public string name_EN { get; set; }
        public string name_NL { get; set; }
        public string name_DE { get; set; }

        public string code { get; set; }
        public string description { get; set; }
        public string description_FR { get; set; }
        public string description_NL { get; set; }
        public string description_EN { get; set; }
        public string description_DE { get; set; }
        public int? score { get; set; }
        public string status { get; set; }
        public string statusMessage { get; set; }
        public string statusMessage_FR { get; set; }
        public string statusMessage_NL { get; set; }
        public string statusMessage_EN { get; set; }
        public string statusMessage_DE { get; set; }
        public ICollection<DtoDimension> dimensions { get; set; }
            
    }
}