using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Revit.Api.Azure.Models.Repositories
{
    public class DimRepository : Repository<Dimension>
    {
        public List<Dimension> GetByCode(string code)
        {
            return DbSet.Where(a => a.code.Contains(code)).ToList();
        }
    }
}