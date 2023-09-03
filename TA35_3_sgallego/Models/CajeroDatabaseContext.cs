using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TA35_3_sgallego.Models;

public partial class CajeroDatabaseContext : DbContext
{
    public CajeroDatabaseContext()
    {
    }

    public CajeroDatabaseContext(DbContextOptions<CajeroDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cajero> Cajeros { get; set; }

    public virtual DbSet<Maquina> Maquinas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=cajero_database;user id=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cajero>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("cajero");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.NomApels)
                .HasMaxLength(255)
                .HasColumnName("nomApels");
        });

        modelBuilder.Entity<Maquina>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("maquina");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Piso).HasColumnName("piso");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.Property(e => e.Codigo)
                .ValueGeneratedNever()
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => new { e.Cajero, e.Producto, e.Maquina })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("venta");

            entity.HasIndex(e => e.Maquina, "maquina");

            entity.HasIndex(e => e.Producto, "producto");

            entity.Property(e => e.Cajero).HasColumnName("cajero");
            entity.Property(e => e.Producto).HasColumnName("producto");
            entity.Property(e => e.Maquina).HasColumnName("maquina");

            entity.HasOne(d => d.CajeroNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Cajero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venta_ibfk_1");

            entity.HasOne(d => d.MaquinaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Maquina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venta_ibfk_3");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venta_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
