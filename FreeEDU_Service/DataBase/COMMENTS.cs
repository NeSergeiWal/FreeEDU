namespace FreeEDU_Service.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class COMMENTS
    {
        public int Id { get; set; }

        public int Course_Id { get; set; }

        public int User_Id { get; set; }

        [Required]
        [StringLength(75)]
        public string Message { get; set; }

        public virtual ACCOUNTS ACCOUNTS { get; set; }

        public virtual COURSES COURSES { get; set; }
    }
}
