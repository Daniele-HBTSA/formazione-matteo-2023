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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=FRIDAY; Initial Catalog=TreeStructureTest; Persist Security Info=True;User ID=sa;Password=Matteo9893");
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
