namespace FBPlusOneBuy.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FanPage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FanPage()
        {
            LivePosts = new HashSet<LivePost>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FanPageName { get; set; }

        [StringLength(128)]
        public string AspNetUserId { get; set; }

        [StringLength(300)]
        public string FbPageLongToken { get; set; }

        [StringLength(50)]
        public string FbMachineId { get; set; }

        [StringLength(20)]
        public string FanPageID { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LivePost> LivePosts { get; set; }
    }
}
