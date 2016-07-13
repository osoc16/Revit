namespace Revit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assessments.Procedure")]
    public partial class Procedure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Procedure()
        {
            ProcedureEnrollment = new HashSet<ProcedureEnrollment>();
        }

        public int Id { get; set; }

        public int TypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public bool IsPublished { get; set; }

        public bool UsesExemptions { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public long TransactionalVersion { get; set; }

        public int? CertificateId { get; set; }

        public int? LanguageProcedureTypeId { get; set; }

        [StringLength(2)]
        public string TestLanguageCode { get; set; }

        [StringLength(2)]
        public string DiplomaLanguageCode { get; set; }

        public int? ArticleId { get; set; }

        [StringLength(8)]
        public string LevelCode { get; set; }

        [StringLength(8)]
        public string DomainCode { get; set; }

        [StringLength(8)]
        public string KnowledgeLevelCode { get; set; }

        public int? SelectionApproachId { get; set; }

        public bool IsTemplate { get; set; }

        public int SubTypeId { get; set; }

        [StringLength(4)]
        public string AccessionLevel { get; set; }

        public bool? GeneratesAccessionCert { get; set; }

        public Guid GlobalId { get; set; }

        public int? ScreeningRequestId { get; set; }

        public bool SendInviteMail { get; set; }

        public int? JobStatuteId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcedureEnrollment> ProcedureEnrollment { get; set; }
    }
}
