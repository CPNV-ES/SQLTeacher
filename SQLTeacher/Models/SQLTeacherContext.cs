using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SQLTeacher.Models
{
    public partial class SQLTeacherContext : DbContext
    {
        public SQLTeacherContext()
        {
        }

        public SQLTeacherContext(DbContextOptions<SQLTeacherContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<Exercises> Exercises { get; set; }
        public virtual DbSet<Participants> Participants { get; set; }
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<Queries> Queries { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Scores> Scores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=SQLTeacher;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classes>(entity =>
            {
                entity.ToTable("classes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("fk_classes_teachers");
            });

            modelBuilder.Entity<Exercises>(entity =>
            {
                entity.ToTable("exercises");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DbScript)
                    .IsRequired()
                    .HasColumnName("db_script")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(5000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Participants>(entity =>
            {
                entity.ToTable("participants");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClasseId).HasColumnName("classe_id");

                entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");

                entity.HasOne(d => d.Classe)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.ClasseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_participants_classes");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_participants_exercices");
            });

            modelBuilder.Entity<People>(entity =>
            {
                entity.ToTable("people");

                entity.HasIndex(e => e.PinCode)
                    .HasName("UQ__people__953E01E993F41486")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Acronym)
                    .HasColumnName("acronym")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ClasseId).HasColumnName("classe_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PinCode).HasColumnName("pin_code");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Classe)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.ClasseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_students_classes");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_persons_roles");
            });

            modelBuilder.Entity<Queries>(entity =>
            {
                entity.ToTable("queries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");

                entity.Property(e => e.Formulation)
                    .IsRequired()
                    .HasColumnName("formulation")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Rank).HasColumnName("rank");

                entity.Property(e => e.Statement)
                    .IsRequired()
                    .HasColumnName("statement")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.Queries)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_queries_exercices");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Scores>(entity =>
            {
                entity.ToTable("scores");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Attempts).HasColumnName("attempts");

                entity.Property(e => e.PeopleId).HasColumnName("people_id");

                entity.Property(e => e.QuerieId).HasColumnName("querie_id");

                entity.Property(e => e.Success).HasColumnName("success");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_scores_persons");

                entity.HasOne(d => d.Querie)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.QuerieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_scores_queries");
            });
        }
    }
}
