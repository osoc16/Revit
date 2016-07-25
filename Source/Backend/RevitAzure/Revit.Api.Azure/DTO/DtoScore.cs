using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Revit.Api.Azure.Models;

namespace Revit.Api.Azure.DTO
{
    public class DtoScore
    {
        public int scoreId { get; set; }
        public int? result { get; set; }

        //final result modified by the jury
        public int? finalResult { get; set; }

        public int? formId { get; set; }
        public int? dimensionId { get; set; }
        public int? competenceId { get; set; }
        public int? candidateId { get; set; }
        public int? juryId { get; set; }
    }
}