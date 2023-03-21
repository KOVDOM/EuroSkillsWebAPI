using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EuroSkillsWebAPI.Models
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

                entity.Property(e => e.OrszagNev)
                    .HasMaxLength(31)
                    .HasColumnName("orszagNev");
            });

            modelBuilder.Entity<Szakma>(entity =>
            {
                entity.ToTable("szakma");

                entity.Property(e => e.Id)
                    .HasMaxLength(2)
                    .HasColumnName("id");

                entity.Property(e => e.SzakmaNev)
                    .HasMaxLength(31)
                    .HasColumnName("szakmaNev");
            });

            modelBuilder.Entity<Versenyzo>(entity =>
            {
                entity.ToTable("versenyzo");

                entity.HasIndex(e => e.OrszagId, "orszagId");

                entity.HasIndex(e => new { e.SzakmaId, e.OrszagId }, "szakmaId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Nev)
                    .HasMaxLength(31)
                    .HasColumnName("nev");

                entity.Property(e => e.OrszagId)
                    .HasMaxLength(2)
                    .HasColumnName("orszagId");

                entity.Property(e => e.Pont)
                    .HasColumnType("int(11)")
                    .HasColumnName("pont");

                entity.Property(e => e.SzakmaId)
                    .HasMaxLength(2)
                    .HasColumnName("szakmaId");

                entity.HasOne(d => d.Orszag)
                    .WithMany(p => p.Versenyzos)
                    .HasForeignKey(d => d.OrszagId)
                    .HasConstraintName("versenyzo_ibfk_2");

                entity.HasOne(d => d.Szakma)
                    .WithMany(p => p.Versenyzos)
                    .HasForeignKey(d => d.SzakmaId)
                    .HasConstraintName("versenyzo_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
