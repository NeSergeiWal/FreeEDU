namespace FreeEDU_Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USERS
    {
        [Key]
        [StringLength(25)]
        public string Login { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(128)]
        public string HashPass { get; set; }

        public virtual ACCOUNTS ACCOUNTS { get; set; }
    }
}
