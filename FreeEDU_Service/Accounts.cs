namespace FreeEDU_Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Accounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accounts()
        {
            Courses = new HashSet<Courses>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Nikname { get; set; }

        [Column("E-mail")]
        [Required]
        [StringLength(50)]
        public string E_mail { get; set; }

        public byte Role { get; set; }

        public int User_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Courses> Courses { get; set; }

        public virtual USERS USERS { get; set; }
    }
}
