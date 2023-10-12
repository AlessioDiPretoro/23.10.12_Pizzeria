namespace InForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            DetailsOrders = new HashSet<DetailsOrders>();
            Products_Ingredients = new HashSet<Products_Ingredients>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string name { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Carica foto")]
        public string photo { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Prezzo")]
        public decimal price { get; set; }

        [Display(Name = "Tempo di preparazione")]
        public TimeSpan deliveryTime { get; set; }

        [StringLength(200)]
        public string ingredients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailsOrders> DetailsOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products_Ingredients> Products_Ingredients { get; set; }
    }
}