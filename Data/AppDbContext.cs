using EmployeeMangerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMangerAPI.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeesProjects { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId,ep.ProjectId });
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep=>ep.Employee)
                .WithMany(e=>e.EmployeeProjects)
                .HasForeignKey(ep=>ep.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .Property(e=>e.Skills)
                .HasConversion(
                v=>string.Join(",",v),
                v=>v.Split(',',StringSplitOptions.RemoveEmptyEntries).ToList());
        } 
    }
}
