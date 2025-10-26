using Employee_Management_System.Models;
using System.Collections.ObjectModel;

namespace Employee_Management_System.Services
{
    public static class DataService
    {
        public static ObservableCollection<Department> Departments { get; private set; }
        public static ObservableCollection<Employee> AllEmployees { get; private set; }

        static DataService()
        {
            Departments = new ObservableCollection<Department>
            {
                new Department { Id = 0, Name = "Wszyscy" },
                new Department { Id = 1, Name = "IT" },
                new Department { Id = 2, Name = "HR" },
                new Department { Id = 3, Name = "Ksiêgowoœæ" }
            };

            AllEmployees = new ObservableCollection<Employee>
            {
                new Employee { FirstName = "Jan", LastName = "Kowalski", Position = "Programista", Department = Departments[1], HireDate = System.DateTime.Now},
                new Employee { FirstName = "Anna", LastName = "Nowak", Position = "Tester", Department = Departments[2], HireDate = System.DateTime.Now},
                new Employee { FirstName = "Piotr", LastName = "Zieliñski", Position = "Architekt", Department = Departments[3], HireDate = System.DateTime.Now}
            };
        }
    }
}