namespace FBPlusOneBuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LineGroup")]
    public partial class LineGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LineGroup()
        {
            Campaigns = new HashSet<Campaign>();
        }

        [Key]
        public int GroupID { get; set; }

        [StringLength(100)]
        public string LineGroupID { get; set; }

        public int StoreManagerID { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public DateTime? JoinDate { get; set; }

        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Campaign> Campaigns { get; set; }

        public virtual StoreManager StoreManager { get; set; }
    }
}
