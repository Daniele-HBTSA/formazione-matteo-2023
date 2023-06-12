using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinanceApp.Context
{
    public partial class FinanceAppContext : DbContext
    {
        public FinanceAppContext()
        {
        }

        public FinanceAppContext(DbContextOptions<FinanceAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aziende> Aziende { get; set; } = null!;
        public virtual DbSet<Movimenti> Movimenti { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Missing config");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aziende>(entity =>
            {
                entity.HasKey(e => e.ID_AZIENDA)
                    .HasName("PK_AZIENDE");

                entity.Property(e => e.ACCOUNT_AZIENDA)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NOME_AZIENDA)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PASSWORD_AZIENDA)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movimenti>(entity =>
            {
                entity.HasKey(e => e.ID_MOVIMENTO)
                    .HasName("PK_MOVIMENTI");

                entity.HasOne(d => d.ID_AZIENDANavigation)
                    .WithMany(p => p.Movimenti)
                    .HasForeignKey(d => d.ID_AZIENDA)
                    .HasConstraintName("FK_CodMov_IdAzienda");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
