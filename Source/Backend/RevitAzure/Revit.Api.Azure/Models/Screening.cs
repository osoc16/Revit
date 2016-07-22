using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models
{
    public class Screening
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


        public virtual Form Form { get; set; }


        public virtual ICollection<Jury> Judges { get; set; }

        
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}