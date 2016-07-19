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


        public virtual Form Form { get; set; }

        public virtual ICollection<Jury> Judges { get; set; }

        
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}