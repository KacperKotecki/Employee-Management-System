using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Models.Domains
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Właściwość nawigacyjna - jedno stanowisko może mieć wielu pracowników
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
