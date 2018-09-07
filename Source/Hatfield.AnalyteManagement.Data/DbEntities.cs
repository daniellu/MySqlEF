namespace Hatfield.AnalyteManagement.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Hatfield.AnalyteManagement.Domain;

    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=EntitiesConnectionString")
        {
        }

        public virtual DbSet<Guideline> Guidelines { get; set; }
        public virtual DbSet<LabAnalyte> LabAnalytes { get; set; }
        public virtual DbSet<AnalyteGuideline> AnalyteGuidelines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LabAnalyte>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Guideline>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Guideline>()
                .HasMany(e => e.AnalyteGuidelines)
                .WithRequired(e => e.Guideline)
                .HasForeignKey(e => e.GuidelineId)
                .WillCascadeOnDelete(true);
        }
    }
}
