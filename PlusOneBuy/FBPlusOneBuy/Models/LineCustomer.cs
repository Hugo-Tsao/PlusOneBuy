namespace FBPlusOneBuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LineCustomer")]
    public partial class LineCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LineCustomer()
        {
            GroupOrderDetails = new HashSet<GroupOrderDetail>();
        }

<<<<<<< HEAD
        [StringLength(128)]
=======
>>>>>>> 17234e48ca7a7df8be8fa64833c2015a4cb0ed13
        public string LineCustomerID { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupOrderDetail> GroupOrderDetails { get; set; }
    }
}
