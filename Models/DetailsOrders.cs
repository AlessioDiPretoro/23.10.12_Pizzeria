namespace InForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DetailsOrders
    {
        public int id { get; set; }

        public int idOrder { get; set; }

        public int idProduct { get; set; }

        public int quatity { get; set; }

        public virtual Products Products { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
