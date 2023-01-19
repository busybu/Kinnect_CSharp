using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SierraDB.Model;

public partial class KinnectContext : DbContext
{
    public KinnectContext()
    {
    }

    public KinnectContext(DbContextOptions<KinnectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<Professor> Professor { get; set; }

    public virtual DbSet<Questo> Questoes { get; set; }

    public virtual DbSet<Resposta> Respostas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CTPC3628\\SQLEXPRESS;Initial Catalog=KINNECT;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Aluno__3214EC275D2C683A");

            entity.ToTable("Aluno");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Senha).IsUnicode(false);
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Modulo__3214EC2743EBC8EB");

            entity.ToTable("Modulo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idprofessor).HasColumnName("IDProfessor");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdprofessorNavigation).WithMany(p => p.Modulos)
                .HasForeignKey(d => d.Idprofessor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IDProfessor");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Professo__3214EC27E971E5DE");

            entity.ToTable("Professor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Senha).IsUnicode(false);
        });

        modelBuilder.Entity<Questo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Questoes__3214EC27212A4616");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descricao).IsUnicode(false);
            entity.Property(e => e.Idmodulo).HasColumnName("IDModulo");
            entity.Property(e => e.Resposta)
                .HasMaxLength(55)
                .IsUnicode(false);

            entity.HasOne(d => d.IdmoduloNavigation).WithMany(p => p.Questos)
                .HasForeignKey(d => d.Idmodulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modulo");
        });

        modelBuilder.Entity<Resposta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resposta__3214EC27362BA8E7");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idaluno).HasColumnName("IDAluno");
            entity.Property(e => e.Idquestoes).HasColumnName("IDQuestoes");
            entity.Property(e => e.Resposta1)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("Resposta");

            entity.HasOne(d => d.IdalunoNavigation).WithMany(p => p.Resposta)
                .HasForeignKey(d => d.Idaluno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IDAluno");

            entity.HasOne(d => d.IdquestoesNavigation).WithMany(p => p.RespostaNavigation)
                .HasForeignKey(d => d.Idquestoes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IDQuestoes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
