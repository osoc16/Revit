using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models
{
    public class Form
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        //public int? Score { get; set; }

        public int? scoreMax { get; set; }


        //public int? finalScore { get; set; }

        public int? finalScoreMax { get; set; }

        public int? finalScoreMin { get; set; }
        public int? MaxCandidates { get; set; }
        public virtual ICollection<Competence> Competences { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}