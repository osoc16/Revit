using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Revit.Api.Azure.Models
{


    public class Dimension
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
      
        public string code { get; set; }

        public string name_FR { get; set; }
        public string name_NL { get; set; }    
        public string name_EN { get; set; }        
        public string name_DE { get; set; }

        public string description_FR { get; set; }
        public string description_NL { get; set; }
        public string description_EN { get; set; }
        public string description_DE { get; set; }


        public virtual ICollection< Competence> Competences { get; set; }

        public virtual ICollection<Score> Scores { get; set; }

    }
}