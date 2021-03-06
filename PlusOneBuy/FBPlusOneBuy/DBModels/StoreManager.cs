namespace FBPlusOneBuy.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StoreManager")]
    public partial class StoreManager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreManager()
        {
            LineGroups = new HashSet<LineGroup>();
        }

        public int StoreManagerID { get; set; }

        [Required]
        [StringLength(128)]
        public string LineID { get; set; }

        [StringLength(128)]
        public string AspNetUserId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(150)]
        public string PictureUrl { get; set; }

        public bool Status { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LineGroup> LineGroups { get; set; }
    }
}
