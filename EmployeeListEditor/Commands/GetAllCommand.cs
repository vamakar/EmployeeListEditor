using System;
using System.Collections.Generic;

namespace EmployeeListEditor.Commands
{
    public class GetAllCommand : Command
    {
        public GetAllCommand(ICollection<Employee> employees)
        {
            Employees = employees;
        }

        public override void Execute()
        {
            foreach (Employee employee in Employees)
            {
                Console.WriteLine($"Id = {employee.Id}, FirstName = {employee.FirstName}, LastName = {employee.LastName}, SalaryPerHour = {employee.SalaryPerHour}");
            }
        }
    }
}
