using System;
using System.Data.Entity;
using System.Linq;

namespace Employee_Management_System
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }


    }

}