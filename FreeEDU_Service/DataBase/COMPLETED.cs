namespace FreeEDU_Service.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMPLETED")]
    public partial class COMPLETED
    {
        public int Id { get; set; }

        public int User_Id { get; set; }

        public int Course_Id { get; set; }

        public virtual ACCOUNTS ACCOUNTS { get; set; }

        public virtual COURSES COURSES { get; set; }
    }
}
