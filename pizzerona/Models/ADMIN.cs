namespace pizzerona.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADMIN")]
    public partial class ADMIN
    {
        [Key]
        public int ID_ADMIN { get; set; }

        [Required]
        [StringLength(50)]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string PASS { get; set; }

        [Required]
        [StringLength(50)]
        public string RUOLO { get; set; }
    }
}
