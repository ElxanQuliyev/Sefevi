namespace Sefevi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TopSlider")]
    public partial class TopSlider
    {
        [Key]
        public int TopSiderId { get; set; }

        [Required]
        [StringLength(250)]
        public string Header { get; set; }

        [StringLength(400)]
        public string Description { get; set; }

        [StringLength(800)]
        public string SliderImage { get; set; }

        public int LanguageId { get; set; }

        public virtual LanguageTB LanguageTB { get; set; }
    }
}
