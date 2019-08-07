namespace FBPlusOneBuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupOrderDetail")]
    public partial class GroupOrderDetail
    {
        public int GroupOrderDetailID { get; set; }

        public int GroupOrderID { get; set; }

        [Required]
        [StringLength(128)]
        public string LineCustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime MessageDateTime { get; set; }

        public virtual GroupOrder GroupOrder { get; set; }

        public virtual LineCustomer LineCustomer { get; set; }
    }
}
