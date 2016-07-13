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
    
    public partial class Procedure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Procedure()
        {
            this.ProcedureEnrollment = new HashSet<ProcedureEnrollment>();
        }
    
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsPublished { get; set; }
        public bool UsesExemptions { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public long TransactionalVersion { get; set; }
        public Nullable<int> CertificateId { get; set; }
        public Nullable<int> LanguageProcedureTypeId { get; set; }
        public string TestLanguageCode { get; set; }
        public string DiplomaLanguageCode { get; set; }
        public Nullable<int> ArticleId { get; set; }
        public string LevelCode { get; set; }
        public string DomainCode { get; set; }
        public string KnowledgeLevelCode { get; set; }
        public Nullable<int> SelectionApproachId { get; set; }
        public bool IsTemplate { get; set; }
        public int SubTypeId { get; set; }
        public string AccessionLevel { get; set; }
        public Nullable<bool> GeneratesAccessionCert { get; set; }
        public System.Guid GlobalId { get; set; }
        public Nullable<int> ScreeningRequestId { get; set; }
        public bool SendInviteMail { get; set; }
        public Nullable<int> JobStatuteId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcedureEnrollment> ProcedureEnrollment { get; set; }
    }
}
