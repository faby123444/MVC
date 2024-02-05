using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConexionAppWeb_Apigateway.Models.DB;

public partial class VolunTrackDbContext : DbContext
{
    public VolunTrackDbContext()
    {
    }

    public VolunTrackDbContext(DbContextOptions<VolunTrackDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EstanteRecompensa> EstanteRecompensas { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<OportunidadesVoluntariado> OportunidadesVoluntariados { get; set; }

    public virtual DbSet<Recompensa> Recompensas { get; set; }

    public virtual DbSet<RegistroEvento> RegistroEventos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Voluntariado> Voluntariados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EstanteRecompensa>(entity =>
        {
            entity.HasKey(e => e.IdEstanteRecompensas);

            entity.Property(e => e.IdEstanteRecompensas).HasColumnName("id_EstanteRecompensas");
            entity.Property(e => e.IdRecompensa).HasColumnName("idRecompensa");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdRecompensaNavigation).WithMany(p => p.EstanteRecompensas)
                .HasForeignKey(d => d.IdRecompensa)
                .HasConstraintName("FK_EstanteRecompensas_Recompensa");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.EstanteRecompensas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_EstanteRecompensas_Usuario");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK_ID_Evento");

            entity.ToTable("Evento");

            entity.Property(e => e.IdEvento).HasColumnName("idEvento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.Lugar)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Título)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OportunidadesVoluntariado>(entity =>
        {
            entity.HasKey(e => e.OportunidadesVoluntariado1);

            entity.ToTable("OportunidadesVoluntariado");

            entity.Property(e => e.OportunidadesVoluntariado1).HasColumnName("OportunidadesVoluntariado");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdVoluntariado).HasColumnName("idVoluntariado");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.OportunidadesVoluntariados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_OportunidadesVoluntariado_Usuario");

            entity.HasOne(d => d.IdVoluntariadoNavigation).WithMany(p => p.OportunidadesVoluntariados)
                .HasForeignKey(d => d.IdVoluntariado)
                .HasConstraintName("FK_OportunidadesVoluntariado_idVoluntariado");
        });

        modelBuilder.Entity<Recompensa>(entity =>
        {
            entity.HasKey(e => e.IdRecompensa).HasName("PK_ID_Recompensa");

            entity.ToTable("Recompensa");

            entity.Property(e => e.IdRecompensa).HasColumnName("idRecompensa");
            entity.Property(e => e.Detalle)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.Título)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RegistroEvento>(entity =>
        {
            entity.HasKey(e => e.RegistroEventos);

            entity.Property(e => e.IdEvento).HasColumnName("idEvento");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.RegistroEventos)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("FK_RegistroEventos_idEvento");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.RegistroEventos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_RegistroEventos_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_ID_Usuario");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.NombreUsuario, "UK_NombreUsuario").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Voluntariado>(entity =>
        {
            entity.HasKey(e => e.IdVoluntariado).HasName("PK_ID_Voluntariado");

            entity.ToTable("Voluntariado");

            entity.Property(e => e.IdVoluntariado).HasColumnName("idVoluntariado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Lugar)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
