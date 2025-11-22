namespace Employee_Management_System.Migrations
{
    using Employee_Management_System.Models.Domains;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Employee_Management_System.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Employee_Management_System.ApplicationDbContext";
        }

        protected override void Seed(Employee_Management_System.ApplicationDbContext context)
        {
           

            context.Departments.AddOrUpdate(x => x.Name,
                new Department { Name = "IT" },
                new Department { Name = "HR" },
                new Department { Name = "Marketing" },
                new Department { Name = "Sprzedaż" },
                new Department { Name = "Finanse" },
                new Department { Name = "Obsługa Klienta" },
                new Department { Name = "Badania i Rozwój" },
                new Department { Name = "Produkcja" },
                new Department { Name = "Logistyka" },
                new Department { Name = "Zarządzanie" }
            );

            
            context.Positions.AddOrUpdate(x => x.Name,
                new Position { Name = "Junior Developer" },
                new Position { Name = "Senior Developer" },
                new Position { Name = "HR Manager"},
                new Position { Name = "Specjalista ds. Sprzedaży" },
                new Position { Name = "Analityk Finansowy" },
                new Position { Name = "Customer Service" },
                new Position { Name = "Research Scientist" },
                new Position { Name = "Production Supervisor" },
                new Position { Name = "Logistics Coordinator" },
                new Position { Name = "Project Manager" }

            );

            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var sb = new System.Text.StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                // Rzucamy nowy błąd, ale tym razem z pełnym opisem problemu
                throw new Exception(sb.ToString());
            }

        }
    }
}
