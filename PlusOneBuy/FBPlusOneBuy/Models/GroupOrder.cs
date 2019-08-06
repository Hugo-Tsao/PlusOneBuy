using System.Web.Script.Serialization;

namespace FBPlusOneBuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupOrder")]
    public partial class GroupOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupOrder()
        {
            GroupOrderDetails = new HashSet<GroupOrderDetail>();
        }

        public int GroupOrderID { get; set; }

        public int CampaignID { get; set; }

        public DateTime OrderDateTime { get; set; }

        [StringLength(10)]
        public string shipDateTime { get; set; }

        public bool isGroup { get; set; }

        public int? NumberOfProduct { get; set; }

        public decimal? Amount { get; set; }

        public virtual Campaign Campaign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupOrderDetail> GroupOrderDetails { get; set; }
    }
}
