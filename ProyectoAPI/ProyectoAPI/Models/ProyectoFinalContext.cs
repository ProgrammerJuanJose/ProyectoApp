using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoAPI.Models;

public partial class ProyectoFinalContext : DbContext
{
    public ProyectoFinalContext()
    {
    }

    public ProyectoFinalContext(DbContextOptions<ProyectoFinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Puntaje> Puntajes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioTipo> UsuarioTipos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //        => optionsBuilder.UseSqlServer("SERVER=JUANJOSECHA; DATABASE=ProyectoFinal; Trusted_Connection=True; TrustServerCertificate =True; INTEGRATED SECURITY=FALSE; USER ID=ProyectoUserApi; PASSWORD=1234");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Puntaje>(entity =>
        {
            entity.HasKey(e => e.PuntajeId).HasName("PK__Puntaje__0D14E087F57633AB");

            entity.ToTable("Puntaje");

            entity.Property(e => e.PuntajeId).HasColumnName("PuntajeID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Puntajes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Puntaje__Puntos__59FA5E80");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE798954937F3");

            entity.ToTable("Usuario");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.UsuarioCedula)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioContrasennia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCorreo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioDireccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioNombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioTelefono)
                .HasMaxLength(255)
                .IsUnicode(false);
            //entity.Property(e => e.UsuarioTipoId).HasColumnName("UsuarioTipoID");

            entity.HasOne(d => d.UsuarioTipo).WithMany(p => p.Usuarios)
                //.HasForeignKey(d => d.UsuarioTipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__Usuario__3B75D760");
        });

        modelBuilder.Entity<UsuarioTipo>(entity =>
        {
            entity.HasKey(e => e.UsuarioTipoId).HasName("PK__UsuarioT__85259CDBF83A5D6F");

            entity.ToTable("UsuarioTipo");

            entity.Property(e => e.UsuarioTipoId)
                .ValueGeneratedNever()
                .HasColumnName("UsuarioTipoID");
            entity.Property(e => e.UsuarioTipoDescripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
