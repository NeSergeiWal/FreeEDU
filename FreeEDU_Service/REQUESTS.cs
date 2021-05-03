namespace FreeEDU_Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class REQUESTS
    {
        [Key]
        [StringLength(25)]
        public string Login { get; set; }

        [Required]
        [StringLength(150)]
        public string Url { get; set; }

        public virtual ACCOUNTS ACCOUNTS { get; set; }
    }
}
