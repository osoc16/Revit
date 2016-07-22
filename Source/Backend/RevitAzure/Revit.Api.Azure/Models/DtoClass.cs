using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Revit.Api.Azure.Models
{
    public class DtoDimension
    {
        public int dimensionId { get; set; }

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

        [JsonProperty(PropertyName = "scores")]
        public ICollection<DtoScore> scoresList { get; set; }
    }

    public class DtoScore
    {
        public int scoreId { get; set; }
        public int? result { get; set; }

        //final result modified by the jury
        public int? finalResult { get; set; }

        public int? formId { get; set; }
        public int? dimensionId { get; set; }
        public int? competenceId { get; set; }
        public int? candidateId { get; set; }
        public int? juryId { get; set; }
    }

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
        public ICollection< DtoCompetence> competencesList { get; set; }
        [JsonProperty(PropertyName = "candidates")]
        public ICollection<DtoCandidate> candidateList { get; set; }
        [JsonProperty(PropertyName = "juries")]
        public ICollection<DtoJury> juryList { get; set; }
        public string name_DE { get; set; }
        public string name_FR { get; set; }
        public string name_NL { get; set; }
        public string name_EN { get; set; }
    }

    public class DtoJury
    {
        public int juryId { get; set; }

        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class DtoCandidate
    {
        public int candidateId { get; set; }

        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string nationalNumber { get; set; }
        public ICollection<DtoJury> juries { get; set; }
    }

    public class DtoCompetence
    {
        public int competenceId { get; set; }
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
        public int weight { get; set; }
        public string comment { get; set; }
    }

    public class DtoScreening
    {
        public int screeningId { get; set; }

        public string code { get; set; }

        public string name { get; set; }
        public string name_FR { get; set; }
        public string name_NL { get; set; }
        public string name_EN { get; set; }
        public string name_DE { get; set; }

        public string description{ get; set; }
        public string description_FR { get; set; }
        public string description_NL { get; set; }
        public string description_EN { get; set; }
        public string description_DE { get; set; }

        [JsonProperty(PropertyName = "juges")]
        public ICollection<DtoJury> jugesList { get; set; }

        [JsonProperty(PropertyName = "candidates")]
        public ICollection<DtoCandidate> candidateList { get; set; }
    }
}
