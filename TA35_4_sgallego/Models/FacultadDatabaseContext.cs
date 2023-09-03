using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TA35_4_sgallego.Models;

public partial class FacultadDatabaseContext : DbContext
{
    public FacultadDatabaseContext()
    {
    }

    public FacultadDatabaseContext(DbContextOptions<FacultadDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Facultad> Facultads { get; set; }

    public virtual DbSet<Investigador> Investigadors { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=facultad_database;user id=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.NumSerie).HasName("PRIMARY");

            entity.ToTable("equipo");

            entity.HasIndex(e => e.Facultad, "facultad");

            entity.Property(e => e.NumSerie)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("numSerie");
            entity.Property(e => e.Facultad).HasColumnName("facultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.FacultadNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.Facultad)
                .HasConstraintName("equipo_ibfk_1");
        });

        modelBuilder.Entity<Facultad>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("facultad");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Investigador>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("PRIMARY");

            entity.ToTable("investigador");

            entity.HasIndex(e => e.Facultad, "facultad");

            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .HasColumnName("dni");
            entity.Property(e => e.Facultad).HasColumnName("facultad");
            entity.Property(e => e.NomApels)
                .HasMaxLength(255)
                .HasColumnName("nomApels");

            entity.HasOne(d => d.FacultadNavigation).WithMany(p => p.Investigadors)
                .HasForeignKey(d => d.Facultad)
                .HasConstraintName("investigador_ibfk_1");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => new { e.Dni, e.NumSerie })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("reserva");

            entity.HasIndex(e => e.NumSerie, "numSerie");

            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .HasColumnName("dni");
            entity.Property(e => e.NumSerie)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("numSerie");
            entity.Property(e => e.Comienzo).HasColumnName("comienzo");
            entity.Property(e => e.Fin).HasColumnName("fin");

            entity.HasOne(d => d.DniNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Dni)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reserva_ibfk_1");

            entity.HasOne(d => d.NumSerieNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.NumSerie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reserva_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
