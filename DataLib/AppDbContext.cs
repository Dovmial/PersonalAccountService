
using DataLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLib
{
    public sealed class AppDbContext: DbContext
    {
        public DbSet<PersonalAccount> PersonalAccounts { get; set; }
        public DbSet<Resident> Residents { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalAccount>(entity =>
            {
                entity.Property(a => a.Number)
                    .IsRequired()
                    .HasMaxLength(10);
                    
                entity.Property(a => a.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(a => a.DateActivate).IsRequired();

                entity.HasIndex(a => a.Number).IsUnique();
                entity.HasIndex(a => a.Address).IsUnique();
            });

            modelBuilder.Entity<Resident>(entity =>
            {
                entity.Property(r => r.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(r => r.SecondName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(r => r.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(p => p.LastName);
                entity.HasIndex(p => new
                {
                    p.LastName,
                    p.FirstName,
                    p.SecondName
                });
            });
        }
    }
}
