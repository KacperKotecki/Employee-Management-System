using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee_Management_System.Models.Domains;

namespace Employee_Management_System.Models.Configurations
{
    public class EmployeeConfigurations : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfigurations()
        {
            ToTable("dbo.Employees");
            HasKey(e => e.Id);
            Property(e => e.FirstName).IsRequired().HasMaxLength(20);
            Property(e => e.LastName).IsRequired().HasMaxLength(20);
            Property(e => e.Email).IsRequired().HasMaxLength(30);
            Property(e => e.Phone).IsOptional().HasMaxLength(15);
            Property(e => e.Salary).IsRequired().HasPrecision(18, 2);
            Property(e => e.HireDate).IsOptional();
            Property(e => e.DismissalDate).IsOptional();
           

        }
    }
}
