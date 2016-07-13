//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Revit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProcedureEnrollment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProcedureEnrollment()
        {
            this.TestSessionEnrollment = new HashSet<TestSessionEnrollment>();
        }
    
        public int Id { get; set; }
        public int ProcedureId { get; set; }
        public int CandidateId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public System.DateTime EnrollmentStartDate { get; set; }
        public Nullable<System.DateTime> EnrollmentEndDate { get; set; }
        public int EnrollmentStatusId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public long TransactionalVersion { get; set; }
        public bool HasDisabilities { get; set; }
        public Nullable<int> OfficialReportId { get; set; }
        public Nullable<int> ClosingReasonId { get; set; }
        public bool HasInvitationMail { get; set; }
        public System.Guid GlobalId { get; set; }
    
        public virtual Procedure Procedure { get; set; }
        public virtual Person Person { get; set; }
        public virtual ProcedureEnrollmentStatus ProcedureEnrollmentStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestSessionEnrollment> TestSessionEnrollment { get; set; }
    }
}
