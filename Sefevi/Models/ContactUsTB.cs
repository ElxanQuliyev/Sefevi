namespace Sefevi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactUsTB")]
    public partial class ContactUsTB
    {
        [Key]
        public int ContactId { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Adress { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(400)]
        public string Details { get; set; }
    }
}
