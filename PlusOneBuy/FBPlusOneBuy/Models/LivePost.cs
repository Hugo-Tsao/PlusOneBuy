namespace FBPlusOneBuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LivePost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LivePost()
        {
            Orders = new HashSet<Order>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int ID { get; set; }

        public string FanPageID { get; set; }

        [StringLength(50)]
        public string LiveName { get; set; }

        public DateTime postTime { get; set; }

        public DateTime? endTime { get; set; }

        [Required]
        [StringLength(50)]
        public string LivePageID { get; set; }

        public decimal? Amount { get; set; }

        public int? QtyOfOrders { get; set; }

        public int? MaxViews { get; set; }

        public virtual FanPage FanPage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
