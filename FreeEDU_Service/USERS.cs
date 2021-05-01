namespace FreeEDU_Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USERS
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Login { get; set; }

        [Required]
        [StringLength(100)]
        public string HashPassword { get; set; }

        public virtual Accounts Accounts { get; set; }
    }
}
