using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RevitApi.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Dimension> Dimensions { get; set; }
    }
}