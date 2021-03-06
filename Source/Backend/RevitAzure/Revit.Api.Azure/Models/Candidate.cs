﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models
{
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

      
        public string nationalNumber { get; set; }

    
        public string firstname { get; set; }

       
        public string lastname { get; set; }

        public virtual ICollection<Screening> Screenings { get; set; }

        public virtual ICollection<Form> Forms { get; set; }
    }
}