namespace Revit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assessments.TargetCriteria")]
    public partial class TargetCriteria
    {
        public int Id { get; set; }

        public int TargetId { get; set; }

        public int? CertificateId { get; set; }

        public double? CaesuraMaxValue { get; set; }

        public double? CaesuraMinValue { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public long TransactionalVersion { get; set; }

        public int? SelectionApproachId { get; set; }

        public int CommunicationTypeId { get; set; }

        public bool CaesuraMinValueChanged { get; set; }

        public bool CaesuraMaxValueChanged { get; set; }

        public virtual Target Target { get; set; }
    }
}
