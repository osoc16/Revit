using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RevitApi.Models
{
    public class Competence
    {


        [JsonProperty(PropertyName="JuryId")]
        public int ID { get; set; }

        [Required]
        public string code { get; set; }

        [Required]
        public string name_FR { get; set; }
        [Required]
        public string name_NL { get; set; }
        [Required]
        public string name_EN { get; set; }
        [Required]
        public string name_DE { get; set; }


        public string description_FR { get; set; }
        public string description_NL { get; set; }
        public string description_EN { get; set; }
        public string description_DE { get; set; }

        public virtual ICollection<Dimension> Dimensions { get; set; }

        public virtual ICollection<Form> Forms { get; set; }
    }
}