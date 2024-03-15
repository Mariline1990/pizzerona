namespace pizzerona.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CLIENTE")]
    public partial class CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENTE()
        {
            ORDINE = new HashSet<ORDINE>();
        }

        [Key]
        public int ID_CLIENTE { get; set; }

        [Required]
        public string NOME { get; set; }

        [Required]
        public string COGNOME { get; set; }

        [Required]
        public string INDIRIZZO { get; set; }

        [Required]
        public string CITTA { get; set; }

        public int CAP { get; set; }

       
        public string RUOLO { get; set; }

        [Required]
        public string EMAIL { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string PASSWORD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDINE> ORDINE { get; set; }
    }
}
