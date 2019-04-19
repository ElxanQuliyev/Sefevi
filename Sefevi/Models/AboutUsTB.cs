namespace Sefevi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AboutUsTB")]
    public partial class AboutUsTB
    {
        [Key]
        public int AboutID { get; set; }

        [Required]
        [StringLength(400)]
        public string Header { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(800)]
        public string AboutImage { get; set; }

        public int? LanguageId { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public virtual LanguageTB LanguageTB { get; set; }
    }
}
