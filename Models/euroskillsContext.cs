using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Kovács_Dominik_backend.Models
{
    public partial class euroskillsContext : DbContext
    {
        public euroskillsContext()
        {
        }

        public euroskillsContext(DbContextOptions<euroskillsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orszag> Orszags { get; set; } = null!;
        public virtual DbSet<Szakma> Szakmas { get; set; } = null!;
        public virtual DbSet<Versenyzo> Versenyzos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;database=euroskills;user=root;password=;ssl mode=none;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orszag>(entity =>
            {
                entity.ToTable("orszag");

                entity.Property(e => e.Id)
                    .HasMaxLength(2)
                    .HasColumnName("id");

                entity.Property(e => e.Orszagnev)
                    .HasMaxLength(31)
                    .HasColumnName("orszagnev");
            });

            modelBuilder.Entity<Szakma>(entity =>
            {
                entity.ToTable("szakma");

                entity.Property(e => e.Id)
                    .HasMaxLength(2)
                    .HasColumnName("id");

                entity.Property(e => e.Szakmanev)
                    .HasMaxLength(31)
                    .HasColumnName("szakmanev");
            });

            modelBuilder.Entity<Versenyzo>(entity =>
            {
                entity.ToTable("versenyzo");

                entity.HasIndex(e => e.Orszagid, "orszagid");

                entity.HasIndex(e => new { e.Szakmaid, e.Orszagid }, "szakmaid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Nev)
                    .HasMaxLength(31)
                    .HasColumnName("nev");

                entity.Property(e => e.Orszagid)
                    .HasMaxLength(2)
                    .HasColumnName("orszagid");

                entity.Property(e => e.Pont)
                    .HasColumnType("int(11)")
                    .HasColumnName("pont");

                entity.Property(e => e.Szakmaid)
                    .HasMaxLength(2)
                    .HasColumnName("szakmaid");

                entity.HasOne(d => d.Orszag)
                    .WithMany(p => p.Versenyzos)
                    .HasForeignKey(d => d.Orszagid)
                    .HasConstraintName("versenyzo_ibfk_1");

                entity.HasOne(d => d.Szakma)
                    .WithMany(p => p.Versenyzos)
                    .HasForeignKey(d => d.Szakmaid)
                    .HasConstraintName("versenyzo_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
