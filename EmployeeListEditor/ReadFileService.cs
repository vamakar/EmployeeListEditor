using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EmployeeListEditor
{
    internal class ReadFileService
    {
        public List<Employee> ReadByFilePath(string filePath)
        {
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, filePath)))
                throw new FileNotFoundException();

            var employeeList = JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(filePath));

            return employeeList;
        }
    }
}
