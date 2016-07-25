using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Revit.Api.Azure.Models;

namespace Revit.Api.Azure.DTO
{
    public class DtoCandidate
    {
        public int candidateId { get; set; }
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string nationalNumber { get; set; }
        public ICollection<DtoJury> juries { get; set; }
    }
}