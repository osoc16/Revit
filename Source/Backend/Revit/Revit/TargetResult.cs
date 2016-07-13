namespace Revit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("r.TargetResult")]
    public partial class TargetResult
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NationalNr { get; set; }

        [Required]
        [StringLength(8)]
        public string SelectionCode { get; set; }

        [StringLength(100)]
        public string TestCode { get; set; }

        [Required]
        [StringLength(50)]
        public string TargetCode { get; set; }

        public bool IsActual { get; set; }

        public double Score { get; set; }

        public DateTime SessionStartedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Feedback { get; set; }

        public bool? IsPassed { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public bool IsExempting { get; set; }

        public bool IsBuffering { get; set; }
    }
}
