﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Squadmakers.Infraestructure.Models;

public partial class SquadmakersContext : DbContext
{
    public SquadmakersContext(DbContextOptions<SquadmakersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chiste> Chistes { get; set; }

    public virtual DbSet<Joke> Jokes { get; set; }

    public virtual DbSet<Tematica> Tematicas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chiste>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Chistes__3214EC079609B590");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Usuario).WithMany(p => p.Chistes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Chistes__Usuario__3D5E1FD2");

            entity.HasMany(d => d.Tematicas).WithMany(p => p.Chistes)
                .UsingEntity<Dictionary<string, object>>(
                    "ChistesTematica",
                    r => r.HasOne<Tematica>().WithMany()
                        .HasForeignKey("TematicaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ChistesTe__Temat__3F466844"),
                    l => l.HasOne<Chiste>().WithMany()
                        .HasForeignKey("ChisteId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ChistesTe__Chist__3E52440B"),
                    j =>
                    {
                        j.HasKey("ChisteId", "TematicaId").HasName("PK__ChistesT__F1DBE87D2C9ABE13");
                        j.ToTable("ChistesTematicas");
                    });
        });

        modelBuilder.Entity<Joke>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Tematica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tematica__3214EC079CCFBA1B");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07A701886B");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}