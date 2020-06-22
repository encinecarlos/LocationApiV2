using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LocationApiV2.Models
{
    public partial class locationContext : DbContext
    {
        public locationContext()
        {
        }

        public locationContext(DbContextOptions<locationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<States> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.ToTable("cities", "location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).HasColumnName("state_id");
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.ToTable("countries", "location");

                entity.HasIndex(e => e.Id)
                    .HasName("countries_id_index");

                entity.HasIndex(e => e.Name)
                    .HasName("countries_name_index");

                entity.HasIndex(e => e.Sortname)
                    .HasName("countries_sortname_index");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150);

                entity.Property(e => e.Phonecode).HasColumnName("phonecode");

                entity.Property(e => e.Sortname)
                    .IsRequired()
                    .HasColumnName("sortname")
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.ToTable("states", "location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
