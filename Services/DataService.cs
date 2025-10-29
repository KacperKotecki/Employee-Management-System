using Employee_Management_System.Models;
using Employee_Management_System.Models.Wrappers;
using System.Collections.ObjectModel;

namespace Employee_Management_System.Services
{
    public static class DataService
    {
        public static ObservableCollection<DepartmentWrapper> Departments { get; private set; }
        public static ObservableCollection<EmployeeWrapper> AllEmployees { get; private set; }

        static DataService()
        {
            Departments = new ObservableCollection<DepartmentWrapper>
            {
                new DepartmentWrapper { Id = 0, Name = "Wszyscy" },
                new DepartmentWrapper { Id = 1, Name = "IT" },
                new DepartmentWrapper { Id = 2, Name = "HR" },
                new DepartmentWrapper { Id = 3, Name = "Ksiêgowoœæ" }
            };

            AllEmployees = new ObservableCollection<EmployeeWrapper>
            {
                new EmployeeWrapper { FirstName = "Jan", LastName = "Kowalski", Position = "Programista", Department = Departments[1], HireDate = System.DateTime.Now},
                new EmployeeWrapper { FirstName = "Anna", LastName = "Nowak", Position = "Tester", Department = Departments[2], HireDate = System.DateTime.Now},
                new EmployeeWrapper { FirstName = "Piotr", LastName = "Zieliñski", Position = "Architekt", Department = Departments[3], HireDate = System.DateTime.Now}
            };
        }
    }
}