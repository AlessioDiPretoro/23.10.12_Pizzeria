namespace InForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            DetailsOrders = new HashSet<DetailsOrders>();
        }

        public int id { get; set; }

        public int idBuyer { get; set; }

        public int idDetailsOrder { get; set; }

        [Required]
        [StringLength(200)]
        public string address { get; set; }

        [StringLength(200)]
        public string notes { get; set; }

        public bool processed { get; set; }

        public DateTime data { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailsOrders> DetailsOrders { get; set; }

        public virtual Users Users { get; set; }
    }
}
