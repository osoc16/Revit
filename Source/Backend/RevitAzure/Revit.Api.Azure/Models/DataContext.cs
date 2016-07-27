using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DBAzure")
        {

        }


        public DbSet<Dimension> Dimensions { get; set; }

        public DbSet<juryCandidateForm> JuryCandidateForms { get; set; }

        public DbSet<Competence> Competences { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Screening> Screenings { get; set; }

       

        public System.Data.Entity.DbSet<Revit.Api.Azure.Models.Form> Forms { get; set; }

        public System.Data.Entity.DbSet<Revit.Api.Azure.Models.Jury> Juries { get; set; }
    }
}