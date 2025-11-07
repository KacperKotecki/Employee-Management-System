using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Models.Domains
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? DismissalDate { get; set; }

        // --- Klucze Obce ---
        public int? DepartmentId { get; set; } // Zmiana na nullable
        public int? PositionId { get; set; }   // Zmiana na nullable

        // --- Właściwości Nawigacyjne ---
        public virtual Department Department { get; set; }
        public virtual Position Position { get; set; }
    }
}
