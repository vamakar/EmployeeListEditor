using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace EmployeeListEditor.Commands
{
    public class DeleteCommand : Command
    {
        private int Id { get; }
        private string FilePath { get; }

        private DeleteCommand(int id, ICollection<Employee> employees, string filePath)
        {
            Id = id;
            Employees = employees;
            FilePath = filePath;
        }

        public static DeleteCommand CreateByArgs(string[] args, ICollection<Employee> employees, string filePath)
        {
            if (args.Length == 2 && args[1].Substring(0, args[1].IndexOf(':')).Equals($"{nameof(Id)}", StringComparison.OrdinalIgnoreCase))
                return new DeleteCommand(Convert.ToInt32(args[1].Substring(args[1].IndexOf(':') + 1)), employees, filePath);

            throw new InvalidOperationException();
        }

        public override void Execute()
        {
            Employee employee = Employees.FirstOrDefault(x => x.Id == Id);
            if (employee != null)
            {
                Employees.Remove(employee);

                string json = JsonConvert.SerializeObject(Employees);
                File.WriteAllText(FilePath, json);
            }
            else
                Console.WriteLine($"Сотрудник с id {Id} не существует");
        }
    }
}
