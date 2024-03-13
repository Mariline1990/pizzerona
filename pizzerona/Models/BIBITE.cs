namespace pizzerona.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BIBITE")]
    public partial class BIBITE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BIBITE()
        {
            ORDINE = new HashSet<ORDINE>();
        }

        [Key]
        public int ID_BIBITA { get; set; }

        [Required]
        [StringLength(10)]
        public string NOME { get; set; }

        [Required]
        public string IMG { get; set; }

        [Required]
        [StringLength(10)]
        public string PREZZO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDINE> ORDINE { get; set; }
    }
}
