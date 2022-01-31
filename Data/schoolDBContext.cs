using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolApp.Dto;

namespace SchoolApp
{
    public partial class schoolDBContext : DbContext
    {
        public schoolDBContext()
        {
        }
    
        public schoolDBContext(DbContextOptions<schoolDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<School> Schools { get; set; } = null!;
        public virtual DbSet<SchoolClass> SchoolClasses { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentsInClass> StudentsInClasses { get; set; } = null!;
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=pitajjovana;database=schoolDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.ClassName).HasMaxLength(250);
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("School");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.SchoolName).HasMaxLength(250);
            });

            modelBuilder.Entity<SchoolClass>(entity =>
            {
                entity.HasIndex(e => e.ClassId, "ClassID");

                entity.HasIndex(e => e.SchoolId, "SchoolID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SchoolClasses)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("schoolclasses_ibfk_2");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolClasses)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("schoolclasses_ibfk_1");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.SchoolId, "SchoolID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.StudentName).HasMaxLength(250);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("student_ibfk_1");
            });

            modelBuilder.Entity<StudentsInClass>(entity =>
            {
                entity.HasIndex(e => e.ClassId, "ClassID");

                entity.HasIndex(e => e.StudentId, "StudentID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentsInClasses)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("studentsinclasses_ibfk_2");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentsInClasses)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("studentsinclasses_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
