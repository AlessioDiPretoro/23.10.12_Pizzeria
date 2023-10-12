namespace InForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products_Ingredients
    {
        public int id { get; set; }

        public int idProduct { get; set; }

        public int idIngredient { get; set; }

        public virtual Ingredients Ingredients { get; set; }

        public virtual Products Products { get; set; }
    }
}
