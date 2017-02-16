namespace Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Domain;

    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=EntitiesConnectionString")
        {
        }

        public virtual DbSet<person> persons { get; set; }
        public virtual DbSet<book> books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<book>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<person>()
                .HasMany(e => e.books)
                .WithRequired(e => e.Owner)
                .HasForeignKey(e => e.OwnerId)
                .WillCascadeOnDelete(true);
        }
    }
}
