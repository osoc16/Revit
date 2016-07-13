namespace Revit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("contacts.Person")]
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            ProcedureEnrollment = new HashSet<ProcedureEnrollment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        [StringLength(2)]
        public string Nationality { get; set; }

        [StringLength(50)]
        public string NationalNr { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string DriversLicence { get; set; }

        public bool HasDisabilities { get; set; }

        public bool RequiresFollowup { get; set; }

        [StringLength(2)]
        public string PreferredLanguageCode { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public long TransactionalVersion { get; set; }

        [StringLength(256)]
        public string ERecruitingUsername { get; set; }

        public Guid GlobalId { get; set; }

        [StringLength(256)]
        public string AddressLine1 { get; set; }

        [StringLength(256)]
        public string AddressLine2 { get; set; }

        [StringLength(256)]
        public string AddressLine3 { get; set; }

        [StringLength(50)]
        public string ZipCode { get; set; }

        [StringLength(50)]
        public string Town { get; set; }

        [StringLength(2)]
        public string CountryCode { get; set; }

        [StringLength(200)]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberIsMobile { get; set; }

        [StringLength(200)]
        public string AltPhoneNumber { get; set; }

        public bool AltPhoneNumberIsMobile { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public string Comments { get; set; }

        [StringLength(2)]
        public string NativeLanguageCode { get; set; }

        [StringLength(1)]
        public string GenderNeutral { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcedureEnrollment> ProcedureEnrollment { get; set; }
    }
}
