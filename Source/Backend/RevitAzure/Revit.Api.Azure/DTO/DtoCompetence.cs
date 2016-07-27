using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Revit.Api.Azure.Models;


namespace Revit.Api.Azure.DTO
{
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
        public int? finalScore { get; set; }
        public string status { get; set; }
        public string statusMessage { get; set; }
        public string statusMessage_FR { get; set; }
        public string statusMessage_NL { get; set; }
        public string statusMessage_EN { get; set; }
        public string statusMessage_DE { get; set; }
        public ICollection<DtoDimension> dimensions { get; set; }
        public int? weight { get; set; }
        public string comment { get; set; }
    }
}