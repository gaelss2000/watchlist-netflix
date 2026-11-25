using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Watchlist.Api.Models;

public partial class WatchlistContext : DbContext
{
    public WatchlistContext(DbContextOptions<WatchlistContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Lista> Listas { get; set; }

    public virtual DbSet<ListaTitulo> ListaTitulos { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<Titulo> Titulos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("generos");

            entity.HasIndex(e => e.Nombre, "nombre").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Lista>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("listas");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Lista)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("listas_ibfk_1");
        });

        modelBuilder.Entity<ListaTitulo>(entity =>
        {
            entity.HasKey(e => new { e.ListaId, e.TituloId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("lista_titulo");

            entity.HasIndex(e => e.TituloId, "titulo_id");

            entity.Property(e => e.ListaId).HasColumnName("lista_id");
            entity.Property(e => e.TituloId).HasColumnName("titulo_id");
            entity.Property(e => e.FechaAgregado)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_agregado");

            entity.HasOne(d => d.Lista).WithMany(p => p.ListaTitulos)
                .HasForeignKey(d => d.ListaId)
                .HasConstraintName("lista_titulo_ibfk_1");

            entity.HasOne(d => d.Titulo).WithMany(p => p.ListaTitulos)
                .HasForeignKey(d => d.TituloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lista_titulo_ibfk_2");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipos");

            entity.HasIndex(e => e.Nombre, "nombre").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Titulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("titulos");

            entity.HasIndex(e => e.TipoId, "tipo_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnioEstreno).HasColumnName("anio_estreno");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(255)
                .HasColumnName("imagen_url");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Sinopsis)
                .HasColumnType("text")
                .HasColumnName("sinopsis");
            entity.Property(e => e.TipoId).HasColumnName("tipo_id");

            entity.HasOne(d => d.Tipo).WithMany(p => p.Titulos)
                .HasForeignKey(d => d.TipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("titulos_ibfk_1");

            entity.HasMany(d => d.Generos).WithMany(p => p.Titulos)
                .UsingEntity<Dictionary<string, object>>(
                    "TituloGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("titulo_genero_ibfk_2"),
                    l => l.HasOne<Titulo>().WithMany()
                        .HasForeignKey("TituloId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("titulo_genero_ibfk_1"),
                    j =>
                    {
                        j.HasKey("TituloId", "GeneroId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("titulo_genero");
                        j.HasIndex(new[] { "GeneroId" }, "genero_id");
                        j.IndexerProperty<int>("TituloId").HasColumnName("titulo_id");
                        j.IndexerProperty<int>("GeneroId").HasColumnName("genero_id");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
