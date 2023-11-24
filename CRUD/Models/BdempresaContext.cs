using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Models;

public partial class BdempresaContext : DbContext
{
    public BdempresaContext()
    {
    }

    public BdempresaContext(DbContextOptions<BdempresaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

 //#warningTo protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
 // => optionsBuilder.UseSqlServer("Server=DESKTOP-2JLPE8T\\SQLEXPRESS; Database=BDEmpresa; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CodigoCargo).HasName("PK__Cargo__FDB545127B6E1194");

            entity.ToTable("Cargo");

            entity.Property(e => e.CodigoCargo).HasColumnName("codigoCargo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("date")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FlagModificado).HasColumnName("flagModificado");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.CodigoEmpresa).HasName("PK__Empresa__B8FA808656EE97CD");

            entity.ToTable("Empresa");

            entity.Property(e => e.CodigoEmpresa).HasColumnName("codigoEmpresa");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaFundacion)
                .HasColumnType("date")
                .HasColumnName("fechaFundacion");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Ruc)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("ruc");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.CodigoEmpleado).HasName("PK__Personal__18674A5B94EE08E1");

            entity.ToTable("Personal");

            entity.Property(e => e.CodigoEmpleado).HasColumnName("codigoEmpleado");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoMaterno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoPaterno");
            entity.Property(e => e.CodigoCargo).HasColumnName("codigoCargo");
            entity.Property(e => e.CodigoEmpresa).HasColumnName("codigoEmpresa");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("date")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");

            entity.HasOne(d => d.CodigoCargoNavigation).WithMany(p => p.Personals)
                .HasForeignKey(d => d.CodigoCargo)
                .HasConstraintName("FK__Personal__codigo__59063A47");

            entity.HasOne(d => d.CodigoEmpresaNavigation).WithMany(p => p.Personals)
                .HasForeignKey(d => d.CodigoEmpresa)
                .HasConstraintName("FK__Personal__codigo__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
