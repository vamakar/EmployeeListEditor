using System;
using System.Collections.Generic;
using EmployeeListEditor.Commands;

namespace EmployeeListEditor
{
    public class CreateCommandService
    {

        private readonly ICollection<Employee> _employees;
        private readonly string _filePath;
        public CreateCommandService(ICollection<Employee> employees, string filePath)
        {
            _employees = employees;
            _filePath = filePath;
        }

        public Command CreateCommandByArguments(string[] args)
        {
            return args[0] switch
            {
                "-add" => AddCommand.CreateByArgs(args, _employees, _filePath),
                "-update" => UpdateCommand.CreateByArgs(args, _employees, _filePath),
                "-get" => GetCommand.CreateByArgs(args, _employees),
                "-delete" => DeleteCommand.CreateByArgs(args, _employees, _filePath),
                "-getall" => new GetAllCommand(_employees),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
