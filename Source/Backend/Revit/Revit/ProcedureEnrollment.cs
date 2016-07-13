namespace Revit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assessments.ProcedureEnrollment")]
    public partial class ProcedureEnrollment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProcedureEnrollment()
        {
            TestSessionEnrollment = new HashSet<TestSessionEnrollment>();
        }

        public int Id { get; set; }

        public int ProcedureId { get; set; }

        public int CandidateId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime EnrollmentStartDate { get; set; }

        public DateTime? EnrollmentEndDate { get; set; }

        public int EnrollmentStatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public long TransactionalVersion { get; set; }

        public bool HasDisabilities { get; set; }

        public int? OfficialReportId { get; set; }

        public int? ClosingReasonId { get; set; }

        public bool HasInvitationMail { get; set; }

        public Guid GlobalId { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual Person Person { get; set; }

        public virtual ProcedureEnrollmentStatus ProcedureEnrollmentStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestSessionEnrollment> TestSessionEnrollment { get; set; }
    }
}
