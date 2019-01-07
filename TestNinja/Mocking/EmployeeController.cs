using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        public EmployeeController()
        {
            
        }

        public ActionResult DeleteEmployee(int id)
        {
            var employeeStorage = new EmployeeStorage();
            employeeStorage.DeleteEmployee(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}