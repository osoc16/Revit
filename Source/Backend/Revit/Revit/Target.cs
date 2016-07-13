namespace Revit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assessments.Target")]
    public partial class Target
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Target()
        {
            Target1 = new HashSet<Target>();
            TargetCriteria = new HashSet<TargetCriteria>();
            Test = new HashSet<Test>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(200)]
        public string Name_nl { get; set; }

        [StringLength(200)]
        public string Name_fr { get; set; }

        [StringLength(200)]
        public string Name_de { get; set; }

        [StringLength(200)]
        public string Name_en { get; set; }

        public double? DefaultCaesuraMinValue { get; set; }

        public double? DefaultCaesuraMaxValue { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public long TransactionalVersion { get; set; }

        public int TypeId { get; set; }

        public int? ParentId { get; set; }

        public bool IsAutoValidatingResults { get; set; }

        public string Description_nl { get; set; }

        public string Description_fr { get; set; }

        public string Description_de { get; set; }

        public string Description_en { get; set; }

        [StringLength(50)]
        public string ModuleType { get; set; }

        [StringLength(50)]
        public string ModuleLevel { get; set; }

        public bool AllowMultipleTestSessionEnrollments { get; set; }

        public bool Archived { get; set; }

        public Guid GlobalId { get; set; }

        public bool IsValidationTarget { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Target> Target1 { get; set; }

        public virtual Target Target2 { get; set; }

        public virtual TargetType TargetType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TargetCriteria> TargetCriteria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test> Test { get; set; }
    }
}
