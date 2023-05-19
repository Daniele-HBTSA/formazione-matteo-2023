using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinanceApp.Context
{
    public partial class LogInContext : DbContext
    {
        public LogInContext()
        {
        }

        public LogInContext(DbContextOptions<LogInContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aziende> Aziende { get; set; } = null!;
        public virtual DbSet<Movimenti> Movimenti { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=FRIDAY;Initial Catalog=FinanceAppDB; Persist Security Info=True;User ID=sa;Password=Matteo9893");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodMov_IdAzienda");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
