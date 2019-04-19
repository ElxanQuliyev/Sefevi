namespace Sefevi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SefeviDB : DbContext
    {
        public SefeviDB()
            : base("name=SefeviDB")
        {
        }

        public virtual DbSet<AboutUsTB> AboutUsTBs { get; set; }
        public virtual DbSet<BlogsTB> BlogsTBs { get; set; }
        public virtual DbSet<ContactUsTB> ContactUsTBs { get; set; }
        public virtual DbSet<LanguageTB> LanguageTBs { get; set; }
        public virtual DbSet<ProductsTB> ProductsTBs { get; set; }
        public virtual DbSet<ServicesTB> ServicesTBs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TopSlider> TopSliders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LanguageTB>()
                .HasMany(e => e.BlogsTBs)
                .WithRequired(e => e.LanguageTB)
                .HasForeignKey(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LanguageTB>()
                .HasMany(e => e.ServicesTBs)
                .WithRequired(e => e.LanguageTB)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LanguageTB>()
                .HasMany(e => e.TopSliders)
                .WithRequired(e => e.LanguageTB)
                .WillCascadeOnDelete(false);
        }
    }
}
