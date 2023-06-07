using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public partial class StudioContext : DbContext
{
    public StudioContext()
    {
    }

    public StudioContext(DbContextOptions<StudioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Администратор> Администраторs { get; set; }

    public virtual DbSet<Запись> Записьs { get; set; }

    public virtual DbSet<Пользователь> Пользовательs { get; set; }

    public virtual DbSet<Услуги> Услугиs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Studio;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        modelBuilder.Entity<Администратор>(entity =>
        {
            entity.HasKey(e => e.Idадминистратора);

            entity.ToTable("Администратор");

            entity.Property(e => e.Idадминистратора).HasColumnName("IDАдминистратора");
            entity.Property(e => e.Логин).HasMaxLength(100);
            entity.Property(e => e.Пароль).HasMaxLength(20);
        });

        modelBuilder.Entity<Запись>(entity =>
        {
            entity.HasKey(e => e.Idзаписи);

            entity.ToTable("Запись");

            entity.Property(e => e.Idзаписи).HasColumnName("IDЗаписи");
            entity.Property(e => e.Idпользователя).HasColumnName("IDПользователя");
            entity.Property(e => e.Idуслуги).HasColumnName("IDУслуги");
            entity.Property(e => e.Время).HasMaxLength(50);
            entity.Property(e => e.Дата).HasColumnType("datetime");
            entity.Property(e => e.Статус).HasMaxLength(50);
            entity.Property(e => e.Телефон).HasMaxLength(11);

            entity.HasOne(d => d.IdпользователяNavigation).WithMany(p => p.Записьs)
                .HasForeignKey(d => d.Idпользователя)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Запись_Пользователь");

            entity.HasOne(d => d.IdуслугиNavigation).WithMany(p => p.Записьs)
                .HasForeignKey(d => d.Idуслуги)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Запись_Услуги");
        });

        modelBuilder.Entity<Пользователь>(entity =>
        {
            entity.HasKey(e => e.Idпользователя);

            entity.ToTable("Пользователь");

            entity.Property(e => e.Idпользователя).HasColumnName("IDПользователя");
            entity.Property(e => e.Имя).HasMaxLength(30);
            entity.Property(e => e.Логин).HasMaxLength(100);
            entity.Property(e => e.Пароль).HasMaxLength(20);
            entity.Property(e => e.Фамилия).HasMaxLength(50);
        });

        modelBuilder.Entity<Услуги>(entity =>
        {
            entity.HasKey(e => e.Idуслуги);

            entity.ToTable("Услуги");

            entity.Property(e => e.Idуслуги).HasColumnName("IDУслуги");
            entity.Property(e => e.Изображение).HasColumnType("image");
            entity.Property(e => e.Название).HasMaxLength(100);
            entity.Property(e => e.Описание).HasMaxLength(300);
            entity.Property(e => e.Цена).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
