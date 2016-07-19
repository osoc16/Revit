using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models
{
    public class juryCandidateForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int form_ID { get; set; }
        [Required]
        public int jury_ID { get; set; }
        [Required]
        public int candidate_ID { get; set; }
    }
}