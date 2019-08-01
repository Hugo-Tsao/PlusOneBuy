namespace FBPlusOneBuy.Models
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

        [StringLength(20)]
        public string FanPageID { get; set; }

        [Required]
        [StringLength(50)]
        public string FanPageName { get; set; }

        [StringLength(128)]
        public string AspNetUserId { get; set; }
        public string FbPageLongToken { get; set; }
        public string FbMachineId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LivePost> LivePosts { get; set; }
    }
}
