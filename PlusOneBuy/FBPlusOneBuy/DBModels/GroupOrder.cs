namespace FBPlusOneBuy.DBModels
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

        public bool isGroup { get; set; }

        public int? NumberOfProduct { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? shipDateTime { get; set; }

        public DateTime? BtnOrderClickDateTime { get; set; }

        public DateTime? BtnGroupClickDateTime { get; set; }

        public virtual Campaign Campaign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupOrderDetail> GroupOrderDetails { get; set; }
    }
}
