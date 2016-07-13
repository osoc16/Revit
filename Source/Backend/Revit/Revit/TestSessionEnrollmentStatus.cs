namespace Revit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assessments.TestSessionEnrollmentStatus")]
    public partial class TestSessionEnrollmentStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestSessionEnrollmentStatus()
        {
            TestSessionEnrollment = new HashSet<TestSessionEnrollment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name_fr { get; set; }

        [Required]
        [StringLength(100)]
        public string Name_nl { get; set; }

        [Required]
        [StringLength(100)]
        public string Name_de { get; set; }

        [Required]
        [StringLength(100)]
        public string Name_en { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestSessionEnrollment> TestSessionEnrollment { get; set; }
    }
}
