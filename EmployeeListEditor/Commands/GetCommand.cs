using System.Collections.Generic;
using System;
using System.Linq;

namespace EmployeeListEditor.Commands
{
    public class GetCommand : Command
    {
        private int Id { get; }

        private GetCommand(int id, ICollection<Employee> employees)
        {
            Id = id;
            Employees = employees;
        }

        public static GetCommand CreateByArgs(string[] args, ICollection<Employee> employees)
        {
            if (args.Length == 2 && args[1].Substring(0, args[1].IndexOf(':')).Equals($"{nameof(Id)}", StringComparison.OrdinalIgnoreCase))
                return new GetCommand(Convert.ToInt32(args[1].Substring(args[1].IndexOf(':') + 1)), employees);

            throw new InvalidOperationException();
        }

        public override void Execute()
        {
            Employee employee = Employees.FirstOrDefault(x => x.Id == Id);
            Console.WriteLine(employee != null
                ? $"Id = {Id}, FirstName = {employee.FirstName}, LastName = {employee.LastName}, SalaryPerHour = {employee.SalaryPerHour}"
                : $"Сотрудник с id {Id} не существует");
        }
    }
}
