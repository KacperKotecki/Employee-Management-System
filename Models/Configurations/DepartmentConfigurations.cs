using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee_Management_System.Models.Domains;
using System.ComponentModel.DataAnnotations.Schema;


namespace Employee_Management_System.Models.Configurations
{
    public class DepartmentConfigurations : EntityTypeConfiguration<Department>
    {
        public DepartmentConfigurations()
        {
            ToTable("dbo.Departments");
            HasKey(d => d.Id);

            
            Property(d => d.Name).IsRequired().HasMaxLength(30);

        }
    }
}
