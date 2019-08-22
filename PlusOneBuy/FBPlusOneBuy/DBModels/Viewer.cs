namespace FBPlusOneBuy.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Viewer
    {
        public int ID { get; set; }

        public DateTime? InfoTime { get; set; }

        public string NumberOfViewers { get; set; }

        public int LiveID { get; set; }

        public virtual LivePost LivePost { get; set; }
    }
}
