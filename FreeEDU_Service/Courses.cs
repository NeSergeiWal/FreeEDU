namespace FreeEDU_Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Courses
    {
        public int Id { get; set; }

        public int Account_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public byte Type { get; set; }

        [Required]
        [StringLength(150)]
        public string Url { get; set; }

        public virtual Accounts Accounts { get; set; }
    }
}
