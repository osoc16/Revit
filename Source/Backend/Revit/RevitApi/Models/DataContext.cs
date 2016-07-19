﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RevitApi.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Dimension> Dimensions { get; set; }

        public DbSet<Competence> Competences { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Screening> Screenings { get; set; }

        public DataContext()
        {
    
        }

        public System.Data.Entity.DbSet<RevitApi.Models.Form> Forms { get; set; }

        public System.Data.Entity.DbSet<RevitApi.Models.Jury> Juries { get; set; }
    }
}