using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models
{
    public class Score
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int? result { get; set; }
        public int? finalResult { get; set; }
        public int? formId { get; set; }
        public int? dimensionId { get; set; }
        public int? competenceId { get; set; }
        public int? candidateId { get; set; }
        public int? juryId { get; set; }
        
            
            
    }
}