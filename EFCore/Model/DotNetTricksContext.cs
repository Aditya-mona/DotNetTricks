using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EFCore.Model
{
    public partial class DotNetTricksContext : DbContext
    {
        public DotNetTricksContext()
        {
        }

        public DotNetTricksContext(DbContextOptions<DotNetTricksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-AADI\\SQLEXPRESS;Database=DotNetTricks;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId).ValueGeneratedNever();

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId).ValueGeneratedNever();

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public Student usp_GetStudent(Guid ?StudentId)
        {
            Student student = new Student();
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_GetStudent";
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("StudentId", StudentId);
                command.Parameters.Add(param);

                Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        student.StudentId = new Guid(reader.GetString("StudentId"));
                        student.StudentName = reader.GetString("Name");
                        student.CourseId= new Guid(reader.GetString("CourseId"));


                    }
                }
                Database.CloseConnection();
            }

            return student;
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
