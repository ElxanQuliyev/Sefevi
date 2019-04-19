namespace Sefevi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServicesTB")]
    public partial class ServicesTB
    {
        [Key]
        public int ServiceID { get; set; }

        [Required]
        [StringLength(250)]
        public string Header { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(800)]
        public string ServiceImage { get; set; }

        public int LanguageId { get; set; }

        public virtual LanguageTB LanguageTB { get; set; }
    }
}
