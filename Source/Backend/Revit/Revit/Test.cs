namespace Revit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assessments.Test")]
    public partial class Test
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Test()
        {
            TestSessionEnrollment = new HashSet<TestSessionEnrollment>();
            Target = new HashSet<Target>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string TestCode { get; set; }

        [StringLength(200)]
        public string Name_nl { get; set; }

        [StringLength(200)]
        public string Name_fr { get; set; }

        [StringLength(200)]
        public string Name_de { get; set; }

        [StringLength(200)]
        public string Name_en { get; set; }

        public string Description_nl { get; set; }

        public string Description_fr { get; set; }

        public string Description_de { get; set; }

        public string Description_en { get; set; }

        public bool IsActive { get; set; }

        public double? MaximumDurationHours { get; set; }

        public int? TestPlatformId { get; set; }

        [StringLength(2)]
        public string TestMethodCode { get; set; }

        public double? ParticipationLimitationFrequenceValue { get; set; }

        [StringLength(2)]
        public string ParticipationLimitationFrequenceUnitTypeCode { get; set; }

        [Required]
        [StringLength(2)]
        public string ParticipationLimitationTypeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public long TransactionalVersion { get; set; }

        public int? ResultEntryTestId { get; set; }

        public int? SaltoId { get; set; }

        public bool Archived { get; set; }

        [StringLength(2)]
        public string LanguageCode { get; set; }

        public int? TestGroupId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestSessionEnrollment> TestSessionEnrollment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Target> Target { get; set; }
    }
}
