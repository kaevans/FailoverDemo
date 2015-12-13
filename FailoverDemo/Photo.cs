namespace FailoverDemo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Photo
    {
        [Key]
        [Column(Order = 0)]
        public Guid PhotoId { get; set; }

        public string Caption { get; set; }

        public string Author { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Uploaded { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool Processed { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Likes { get; set; }
    }
}
