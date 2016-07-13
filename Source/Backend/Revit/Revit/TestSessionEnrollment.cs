namespace Revit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assessments.TestSessionEnrollment")]
    public partial class TestSessionEnrollment
    {
        public int Id { get; set; }

        [StringLength(4000)]
        public string AbsenceMotivation { get; set; }

        public DateTime Appointment { get; set; }

        public DateTime SubscribedSince { get; set; }

        public int? EnrollmentStatusId { get; set; }

        public int EventId { get; set; }

        public int ProcedureEnrollmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public long TransactionalVersion { get; set; }

        public int TestId { get; set; }

        [StringLength(8)]
        public string ADM_ex_code { get; set; }

        public bool NotifyCandidate { get; set; }

        public virtual ProcedureEnrollment ProcedureEnrollment { get; set; }

        public virtual Test Test { get; set; }

        public virtual TestSessionEnrollmentStatus TestSessionEnrollmentStatus { get; set; }
    }
}
