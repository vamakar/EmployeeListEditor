using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmployeeListEditor.Commands
{
    public class AddCommand : Command
    {
        private string FirstName { get; }
        private string LastName { get; }
        private decimal Salary { get; }
        private string FilePath { get; }

        private AddCommand(string firstName, string lastName, decimal salary, ICollection<Employee> employees, string filePath)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            Employees = employees;
            FilePath = filePath;
        }

        public static AddCommand CreateByArgs(string[] args, ICollection<Employee> employees, string filePath)
        {
            var possibleArguments = new List<string> { $"{nameof(FirstName)}", $"{nameof(LastName)}", $"{nameof(Salary)}" };
            
            string firstName = default;
            string lastName = default;
            decimal salary = default;
            for (var i = 1; i < args.Length; i++)
            {
                string parameter = args[i].Substring(0, args[i].IndexOf(':'));
                if (!possibleArguments.Any(x => x.Equals(parameter, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException();

                if (parameter.Equals($"{nameof(FirstName)}", StringComparison.OrdinalIgnoreCase))
                {
                    firstName = args[i].Substring(args[i].IndexOf(':') + 1);
                    possibleArguments.Remove($"{nameof(FirstName)}");
                }
                else if (parameter.Equals($"{nameof(LastName)}", StringComparison.OrdinalIgnoreCase))
                {
                    lastName = args[i].Substring(args[i].IndexOf(':') + 1);
                    possibleArguments.Remove($"{nameof(FirstName)}");
                }
                else if (parameter.Equals($"{nameof(Salary)}", StringComparison.OrdinalIgnoreCase))
                {
                    salary = Convert.ToDecimal(args[i].Substring(args[i].IndexOf(':') + 1));
                    possibleArguments.Remove($"{nameof(Salary)}");
                }
                else throw new InvalidOperationException();

                possibleArguments.Remove(parameter);
            }

            return new AddCommand(firstName, lastName, salary, employees, filePath);
        }

        public override void Execute()
        {
            int maxId = Employees.Max(x => x.Id);
            Employees.Add(new Employee(maxId + 1, FirstName, FirstName, Salary));

            string json = JsonConvert.SerializeObject(Employees);
            File.WriteAllText(FilePath, json);
        }
    }
}
