using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Revit.Api.Azure.Models;

namespace Revit.Api.Azure.DTO
{
    public class DtoScreening
    {
        public int screeningId { get; set; }

        public string code { get; set; }

        public string name { get; set; }
        public string name_FR { get; set; }
        public string name_NL { get; set; }
        public string name_EN { get; set; }
        public string name_DE { get; set; }

        public string description { get; set; }
        public string description_FR { get; set; }
        public string description_NL { get; set; }
        public string description_EN { get; set; }
        public string description_DE { get; set; }
        public DtoForm form { get; set; }


        [JsonProperty(PropertyName = "juries")]
        public ICollection<DtoJury> jugesList { get; set; }

        [JsonProperty(PropertyName = "candidates")]
        public ICollection<DtoCandidate> candidateList { get; set; }
    }
}