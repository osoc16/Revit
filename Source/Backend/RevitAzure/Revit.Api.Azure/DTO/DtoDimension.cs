using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Revit.Api.Azure.Models;

namespace Revit.Api.Azure.DTO
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
}