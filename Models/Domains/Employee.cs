using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;

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
        public DateTime HireDate { get; set; }
        public DateTime? DismissalDate { get; set; }

        // --- Klucze Obce ---
        // Jawne zdefiniowanie kluczy obcych daje lepszą kontrolę.
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }

        // --- Właściwości Nawigacyjne ---
        // Mówią Entity Framework, jak połączyć te klucze z odpowiednimi tabelami.
        // Słowo kluczowe 'virtual' pozwala na tzw. "leniwe ładowanie" (lazy loading).
        public virtual Department Department { get; set; }
        public virtual Position Position { get; set; }
    }
}
