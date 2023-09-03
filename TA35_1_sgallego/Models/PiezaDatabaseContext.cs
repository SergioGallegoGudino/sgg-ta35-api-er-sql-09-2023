using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TA35_1_sgallego.Models;

public partial class PiezaDatabaseContext : DbContext
{
    public PiezaDatabaseContext()
    {
    }

    public PiezaDatabaseContext(DbContextOptions<PiezaDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pieza> Piezas { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Suministra> Suministras { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=pieza_database;user id=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Pieza>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("pieza");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proveedor");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Suministra>(entity =>
        {
            entity.HasKey(e => new { e.CodigoPieza, e.ProveedorId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("suministra");

            entity.Property(e => e.CodigoPieza).HasColumnName("codigoPieza");
            entity.Property(e => e.ProveedorId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("proveedorId");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.CodigoPiezaNavigation).WithMany(p => p.Suministras)
                .HasForeignKey(d => d.CodigoPieza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("suministra_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
