namespace FreeEDU_Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACCOUNTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCOUNTS()
        {
            COURSES = new HashSet<COURSES>();
        }

        [Key]
        [StringLength(25)]
        public string Login { get; set; }

        [Required]
        [StringLength(3)]
        public string Role { get; set; }

        [Required]
        [StringLength(150)]
        public string Url { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COURSES> COURSES { get; set; }

        public virtual REQUESTS REQUESTS { get; set; }

        public virtual USERS USERS { get; set; }
    }
}
