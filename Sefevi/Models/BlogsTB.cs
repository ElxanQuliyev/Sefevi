namespace Sefevi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BlogsTB")]
    public partial class BlogsTB
    {
        [Key]
        public int BlogID { get; set; }

        [Required]
        [StringLength(600)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(800)]
        public string BlogImage { get; set; }

        public int Language { get; set; }

        public virtual LanguageTB LanguageTB { get; set; }
    }
}
