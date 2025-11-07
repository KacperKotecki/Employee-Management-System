using Employee_Management_System.Models;
using Employee_Management_System.Models.Domains;
using Employee_Management_System.Models.Wrappers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Data.Entity;
using Employee_Management_System.Models.Converters;

namespace Employee_Management_System.Services
{
    public class DataService
    {
        public List<Department> GetDepartments()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Departments.ToList();
               

            }
        }

        public List<Position> GetPositions()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Positions.ToList();
            }
        }

        public List<EmployeeWrapper> GetEmployees(int departmentId = 0, int positionId = 0)
        {
            using (var context = new ApplicationDbContext())
            {
                var employees = context
                    .Employees
                    .Include(e => e.Department)
                    .Include(e => e.Position)
                    .AsQueryable();

                if (departmentId != 0)
                {
                    employees = employees.Where(e => e.DepartmentId == departmentId);
                }

                if (positionId != 0)
                {
                    employees = employees.Where(e => e.PositionId == positionId);
                }

                return employees
                    .ToList()
                    .Select(x => x.ToWrapper()).ToList();
            }
        }

        public void DismissalEmployee(int employeeId)
        {
            using (var context = new ApplicationDbContext())
            {
                var employee = context.Employees.Find(employeeId);
                if (employee != null)
                {
                    employee.DismissalDate = System.DateTime.Now;
                    context.SaveChanges();
                }
            }
        }

        public void AddUpdateEmployee(EmployeeWrapper employeeWrapper)
        {
            using (var context = new ApplicationDbContext())
            {
                if (employeeWrapper.Id == 0) 
                {
                    var newEmployee = employeeWrapper.ToDao();
                    context.Employees.Add(newEmployee);
                }
                else 
                {
                    var employeeToUpdate = context.Employees.Find(employeeWrapper.Id);
                    if (employeeToUpdate != null)
                    {
                        employeeToUpdate.FirstName = employeeWrapper.FirstName;
                        employeeToUpdate.LastName = employeeWrapper.LastName;
                        employeeToUpdate.Email = employeeWrapper.Email;
                        employeeToUpdate.Phone = employeeWrapper.Phone;
                        employeeToUpdate.Salary = employeeWrapper.Salary;
                        employeeToUpdate.HireDate = employeeWrapper.HireDate;
                        employeeToUpdate.DepartmentId = employeeWrapper.Department.Id;
                        employeeToUpdate.PositionId = employeeWrapper.Position.Id;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}