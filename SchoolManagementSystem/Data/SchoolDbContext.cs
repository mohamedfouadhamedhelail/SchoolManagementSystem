using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Data
{
  public class SchoolDbContext : DbContext
  {
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();

    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Student>()
          .HasIndex(s => s.Email)
          .IsUnique();

      modelBuilder.Entity<Student>()
          .HasOne(s => s.Department)
          .WithMany(d => d.Students)
          .HasForeignKey(s => s.DepartmentId)
          .OnDelete(DeleteBehavior.SetNull);

      modelBuilder.Entity<Department>()
          .HasIndex(d => d.Name)
          .IsUnique();
    }
  }
}
