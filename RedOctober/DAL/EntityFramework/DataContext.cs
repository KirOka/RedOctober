using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using RedOctober.DAL.Entities;

namespace RedOctober.DAL.EntityFramework
{
    /// <summary>
    /// Контекст
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Department> Departments { get; set; } = null!;

        public DbSet<Position> Positions { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
            if (!Departments.Any())
            {
                Departments.Add(new Department() { Name = "Заводоуправление", ParentDepartment = null });
                this.SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>()
                .HasOne(p => p.Department)
                .WithMany(t => t.Positions)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
