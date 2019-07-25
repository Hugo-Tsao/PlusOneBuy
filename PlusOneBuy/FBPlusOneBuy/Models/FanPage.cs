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

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string FanPageID { get; set; }

        [Required]
        [StringLength(50)]
        public string FanPageName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LivePost> LivePosts { get; set; }
    }
}
