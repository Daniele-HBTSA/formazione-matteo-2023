using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LogInDotNet.Context
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

        public virtual DbSet<Users> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Config missing");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserID);

                entity.Property(e => e.UserName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserPsw)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
