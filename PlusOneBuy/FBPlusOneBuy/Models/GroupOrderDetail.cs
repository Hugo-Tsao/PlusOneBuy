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
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupOrderID { get; set; }

        [Key]
        [Column(Order = 1)]
<<<<<<< HEAD
        [StringLength(128)]
=======
>>>>>>> 17234e48ca7a7df8be8fa64833c2015a4cb0ed13
        public string LineCustomerID { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        public DateTime? MessageDateTime { get; set; }

        public virtual GroupOrder GroupOrder { get; set; }

        public virtual LineCustomer LineCustomer { get; set; }
    }
}
