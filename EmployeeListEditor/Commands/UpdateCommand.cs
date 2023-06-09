using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace EmployeeListEditor.Commands
{
    public class UpdateCommand : Command
    {
        private int Id { get; }
        private string FirstName { get; }
        private string LastName { get; }
        private decimal Salary { get; }
        private string FilePath { get; }

        private UpdateCommand(int id, string firstName, string lastName, decimal salary, ICollection<Employee> employees, string filePath)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            Employees = employees;
            FilePath = filePath;
        }

        public static UpdateCommand CreateByArgs(string[] args, ICollection<Employee> employees, string filePath)
        {
            var possibleArguments = new List<string>{ $"{nameof(Id)}", $"{nameof(FirstName)}" , $"{nameof(LastName)}" , $"{nameof(Salary)}" };

            int id = default;
            string firstName = default;
            string lastName = default;
            decimal salary = default;
            for (var i = 1; i < args.Length; i++)
            {
                string parameter = args[i].Substring(0, args[i].IndexOf(':'));
                if (!possibleArguments.Any(x => x.Equals(parameter, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException();

                if (parameter.Equals($"{nameof(Id)}", StringComparison.OrdinalIgnoreCase))
                {
                    id = Convert.ToInt32(args[i].Substring(args[i].IndexOf(':') + 1));
                    possibleArguments.Remove($"{nameof(Id)}");
                }
                else if (parameter.Equals($"{nameof(FirstName)}", StringComparison.OrdinalIgnoreCase))
                {
                    firstName = args[i].Substring(args[i].IndexOf(':') + 1);
                    possibleArguments.Remove($"{nameof(FirstName)}");
                }
                else if (parameter.Equals($"{nameof(LastName)}", StringComparison.OrdinalIgnoreCase))
                {
                    lastName = args[i].Substring(args[i].IndexOf(':') + 1);
                    possibleArguments.Remove($"{nameof(LastName)}");
                }
                else if (parameter.Equals($"{nameof(Salary)}", StringComparison.OrdinalIgnoreCase))
                {
                    salary = Convert.ToDecimal(args[i].Substring(args[i].IndexOf(':') + 1));
                    possibleArguments.Remove($"{nameof(Salary)}");
                }
                else throw new InvalidOperationException();
            }

            if (possibleArguments.Contains($"{nameof(Id)}"))
                throw new InvalidOperationException();

            return new UpdateCommand(id, firstName, lastName, salary, employees, filePath);
        }

        public override void Execute()
        {
            Employee toUpdate = Employees.FirstOrDefault(x => x.Id == Id);
            if (toUpdate != null)
            {
                if (FirstName != default) toUpdate.FirstName = FirstName;
                if (LastName != default) toUpdate.LastName = LastName;
                if (Salary != default) toUpdate.SalaryPerHour = Salary;

                string json = JsonConvert.SerializeObject(Employees);
                File.WriteAllText(FilePath, json);
            }
            else
                Console.WriteLine($"Сотрудник с id {Id} не существует");
        }
    }
}
