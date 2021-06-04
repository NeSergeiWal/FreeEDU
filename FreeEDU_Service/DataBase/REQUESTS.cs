namespace FreeEDU_Service.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class REQUESTS
    {
        public int Id { get; set; }

        public int Account_Id { get; set; }

        public virtual ACCOUNTS ACCOUNTS { get; set; }
    }
}
