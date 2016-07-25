using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Revit.Api.Azure.Models;

namespace Revit.Api.Azure.DTO
{
    public class DtoForm
    {
        public int? maxCandidates { get; set; }

        public int formId { get; set; }

        public string name { get; set; }
        public string code { get; set; }

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
        [JsonProperty(PropertyName = "competences")]
        public ICollection<DtoCompetence> competencesList { get; set; }
        [JsonProperty(PropertyName = "candidates")]
        public ICollection<DtoCandidate> candidateList { get; set; }
        [JsonProperty(PropertyName = "juries")]
        public ICollection<DtoJury> juryList { get; set; }
        public string name_DE { get; set; }
        public string name_FR { get; set; }
        public string name_NL { get; set; }
        public string name_EN { get; set; }
    }
}