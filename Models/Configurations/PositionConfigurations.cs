using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee_Management_System.Models.Domains;

namespace Employee_Management_System.Models.Configurations
{
    public class PositionConfigurations : EntityTypeConfiguration<Position>
    {
        public PositionConfigurations()
        {
            ToTable("dbo.Positions");
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(30);
        }
    }
}
