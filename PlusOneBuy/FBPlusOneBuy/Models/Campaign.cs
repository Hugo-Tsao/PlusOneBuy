namespace FBPlusOneBuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Campaign")]
    public partial class Campaign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Campaign()
        {
            GroupOrders = new HashSet<GroupOrder>();
        }

        public int CampaignID { get; set; }

        public int GroupID { get; set; }

        public int ProductID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public int ProductSet { get; set; }

        public int ProductGroup { get; set; }

        [Required]
        [StringLength(50)]
        public string Keyword { get; set; }

        public DateTime PostTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Detail { get; set; }

        public virtual LineGroup LineGroup { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupOrder> GroupOrders { get; set; }
    }
}
