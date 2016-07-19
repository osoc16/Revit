using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models
{
    public class Competence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[JsonProperty(PropertyName="JuryId")]
        public int ID { get; set; }

        public string code { get; set; }

        //weight of the competence
        public int weight { get; set; }

        //name of the competence
        public string name_FR { get; set; }       
        public string name_NL { get; set; }
        public string name_EN { get; set; }
        public string name_DE { get; set; }

        //description of the competence
        public string description_FR { get; set; }
        public string description_NL { get; set; }
        public string description_EN { get; set; }
        public string description_DE { get; set; }


        public string status { get; set; }

        public string statusMessage_FR { get; set; }
        public string statusMessage_NL { get; set; }
        public string statusMessage_EN { get; set; }
        public string statusMessage_DE { get; set; }


        public string comment { get; set; }
        public virtual ICollection<Dimension> Dimensions { get; set; }

        public virtual ICollection<Form> Forms { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}