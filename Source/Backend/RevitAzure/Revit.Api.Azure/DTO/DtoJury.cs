using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Revit.Api.Azure.Models;

namespace Revit.Api.Azure.DTO
{
    public class DtoJury
    {
        public int juryId { get; set; }

        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}