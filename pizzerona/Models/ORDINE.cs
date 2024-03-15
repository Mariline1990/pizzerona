namespace pizzerona.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDINE")]
    public partial class ORDINE
    {
        [Key]
        public int ID_ORDINE { get; set; }

        public int? FK_ID_PIZZA { get; set; }

        public int? FK_ID_BIBITA { get; set; }

        public int? FK_ID_CLIENTE { get; set; }

        public string INDIRIZZO_CONSEGNA { get; set; }

        public int? QUANTITA { get; set; }

        [StringLength(10)]
        public string NOTA { get; set; }

        public decimal? TOTALE { get; set; }

        public virtual BIBITE BIBITE { get; set; }

        public virtual CLIENTE CLIENTE { get; set; }

        public virtual Pizze Pizze { get; set; }
    }
}
