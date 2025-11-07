using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee_Management_System.Models.Domains;
using Employee_Management_System.Models.Wrappers;

namespace Employee_Management_System.Models.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeWrapper ToWrapper(this Employee employee)
        {
            if (employee == null)
                return null;
            return new EmployeeWrapper
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                HireDate = employee.HireDate ?? DateTime.MinValue,
                DismissalDate = employee.DismissalDate,
                Department = employee.Department != null ? new DepartmentWrapper
                {
                    Id = employee.Department.Id,
                    Name = employee.Department.Name
                } : null,
                Position = employee.Position != null ? new PositionWrapper
                {
                    Id = employee.Position.Id,
                    Name = employee.Position.Name
                } : null
            };
        }

        public static Employee ToDao(this EmployeeWrapper wrapper)
        {
            if (wrapper == null)
                return null;
            return new Employee
            {
                Id = wrapper.Id,
                FirstName = wrapper.FirstName,
                LastName = wrapper.LastName,
                Email = wrapper.Email,
                Phone = wrapper.Phone,
                PositionId = (wrapper.Position != null && wrapper.Position.Id != 0) ? wrapper.Position.Id : (int?)null,
                DepartmentId = (wrapper.Department != null && wrapper.Department.Id != 0) ? wrapper.Department.Id : (int?)null,
                Salary = wrapper.Salary,
                HireDate = wrapper.HireDate,
                DismissalDate = wrapper.DismissalDate
            };
        }
    }
}
