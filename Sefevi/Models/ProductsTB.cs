namespace Sefevi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductsTB")]
    public partial class ProductsTB
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string Decription { get; set; }

        [Required]
        [StringLength(50)]
        public string Protein { get; set; }

        [Required]
        [StringLength(50)]
        public string Price { get; set; }

        [StringLength(50)]
        public string Ton { get; set; }

        public int? LanguageId { get; set; }

        [StringLength(800)]
        public string ProductImage { get; set; }

        public virtual LanguageTB LanguageTB { get; set; }
    }
}
