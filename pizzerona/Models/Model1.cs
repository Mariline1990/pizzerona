using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace pizzerona.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ADMIN> ADMIN { get; set; }
        public virtual DbSet<BIBITE> BIBITE { get; set; }
        public virtual DbSet<CLIENTE> CLIENTE { get; set; }
        public virtual DbSet<ORDINE> ORDINE { get; set; }
        public virtual DbSet<Pizze> Pizze { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BIBITE>()
                .Property(e => e.NOME)
                .IsFixedLength();

            modelBuilder.Entity<BIBITE>()
                .Property(e => e.PREZZO)
                .IsFixedLength();

            modelBuilder.Entity<BIBITE>()
                .HasMany(e => e.ORDINE)
                .WithOptional(e => e.BIBITE)
                .HasForeignKey(e => e.FK_ID_BIBITA);

            modelBuilder.Entity<CLIENTE>()
                .HasMany(e => e.ORDINE)
                .WithRequired(e => e.CLIENTE)
                .HasForeignKey(e => e.FK_ID_CLIENTE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ORDINE>()
                .Property(e => e.NOTA)
                .IsFixedLength();

            modelBuilder.Entity<ORDINE>()
                .Property(e => e.TOTALE)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Pizze>()
                .Property(e => e.Prezzo);
                

            modelBuilder.Entity<Pizze>()
                .Property(e => e.Ingredienti)
                .IsFixedLength();

            modelBuilder.Entity<Pizze>()
                .HasMany(e => e.ORDINE)
                .WithRequired(e => e.Pizze)
                .HasForeignKey(e => e.FK_ID_PIZZA)
                .WillCascadeOnDelete(false);
        }
    }
}
