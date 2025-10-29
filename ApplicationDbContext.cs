using System;
using System.Data.Entity;
using System.Linq;
using Employee_Management_System.Models.Domains;

namespace Employee_Management_System
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Konfiguracje encji
            modelBuilder.Configurations.Add(new Models.Configurations.EmployeeConfigurations());
            modelBuilder.Configurations.Add(new Models.Configurations.DepartmentConfigurations());
            modelBuilder.Configurations.Add(new Models.Configurations.PositionConfigurations());
            base.OnModelCreating(modelBuilder);
        }

    }

}