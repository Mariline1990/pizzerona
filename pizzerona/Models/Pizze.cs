namespace pizzerona.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pizze")]
    public partial class Pizze
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pizze()
        {
            ORDINE = new HashSet<ORDINE>();
        }

        [Key]
        public int id_Pizza { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string img { get; set; }

        [Required]
      
        public decimal Prezzo { get; set; }

        public int TempoConsegna { get; set; }

        [Required]
       
        public string Ingredienti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDINE> ORDINE { get; set; }
    }
}
