using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnterpriseHierarchy.Context
{
    public partial class EnterpriseHierarchyContext : DbContext
    {
        public EnterpriseHierarchyContext()
        {
        }

        public EnterpriseHierarchyContext(DbContextOptions<EnterpriseHierarchyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ENTERPRISES> ENTERPRISES { get; set; } = null!;
        public virtual DbSet<ENT_MOVMENTS> ENT_MOVMENTS { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Missing configuration");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ENTERPRISES>(entity =>
            {
                entity.HasKey(e => e.ID_ENTERPRISE);

                entity.HasIndex(e => e.ENT_CODE, "IX_ENTERPRISES")
                    .IsUnique();

                entity.Property(e => e.ENT_ADDRESS)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ENT_CODE)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ENT_NAME)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ENT_MOVMENTS>(entity =>
            {
                entity.HasKey(e => e.ID_MOVMENT);

                entity.Property(e => e.MOV_CAUSAL)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ID_ENTERPRISENavigation)
                    .WithMany(p => p.ENT_MOVMENTS)
                    .HasForeignKey(d => d.ID_ENTERPRISE)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ENTERPRISE_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
