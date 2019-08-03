namespace FBPlusOneBuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupManager")]
    public partial class GroupManager
    {
        [Key]
        public string UserId { get; set; }

        [StringLength(128)]
        public string AspNetUserId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(150)]
        public string PictureUrl { get; set; }
    }
}
