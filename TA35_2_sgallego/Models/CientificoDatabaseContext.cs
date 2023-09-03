using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TA35_2_sgallego.Models;

public partial class CientificoDatabaseContext : DbContext
{
    public CientificoDatabaseContext()
    {
    }

    public CientificoDatabaseContext(DbContextOptions<CientificoDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cientifico> Cientificos { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=cientifico_database;user id=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cientifico>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("PRIMARY");

            entity.ToTable("cientifico");

            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .HasColumnName("dni");
            entity.Property(e => e.NomApels)
                .HasMaxLength(255)
                .HasColumnName("nomApels");

            entity.HasMany(d => d.Proyectos).WithMany(p => p.Cientificos)
                .UsingEntity<Dictionary<string, object>>(
                    "Asignado",
                    r => r.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("Proyecto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("asignado_ibfk_2"),
                    l => l.HasOne<Cientifico>().WithMany()
                        .HasForeignKey("Cientifico")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("asignado_ibfk_1"),
                    j =>
                    {
                        j.HasKey("Cientifico", "Proyecto")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("asignado");
                        j.HasIndex(new[] { "Proyecto" }, "proyecto");
                        j.IndexerProperty<string>("Cientifico")
                            .HasMaxLength(8)
                            .HasColumnName("cientifico");
                        j.IndexerProperty<string>("Proyecto")
                            .HasMaxLength(4)
                            .IsFixedLength()
                            .HasColumnName("proyecto");
                    });
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyecto");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Horas).HasColumnName("horas");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
