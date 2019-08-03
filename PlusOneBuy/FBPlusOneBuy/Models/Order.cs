namespace FBPlusOneBuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        [StringLength(30)]
        public string CustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string Keyword { get; set; }

        public DateTime OrderDateTime { get; set; }

        public int Quantity { get; set; }

        public int LiveID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual LivePost LivePost { get; set; }

        public virtual Product Product { get; set; }
    }
}
