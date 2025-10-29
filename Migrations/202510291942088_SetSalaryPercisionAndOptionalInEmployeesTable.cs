namespace Employee_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetSalaryPercisionAndOptionalInEmployeesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Salary", c => c.Decimal(precision: 16, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Salary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
