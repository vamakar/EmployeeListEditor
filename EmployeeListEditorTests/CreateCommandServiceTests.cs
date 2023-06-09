using System;
using System.Collections.Generic;
using EmployeeListEditor;
using EmployeeListEditor.Commands;
using NUnit.Framework;

namespace EmployeeListEditorTests
{
    public class CreateCommandServiceTests
    {
        [Test]
        public void CreateCommandByArguments_AddArgument_CreatesAddCommand()
        {
            var filePath = "..\\..\\Resources\\TestEmployeeList.json";
            var service = new CreateCommandService(new List<Employee>(), filePath);

            var args = new []{ "-add", "FirstName:John", "LastName:Doe", "Salary:100,50" } ;
            var command = service.CreateCommandByArguments(args);

            Assert.IsInstanceOf<AddCommand>(command);
        }

        [Test]
        public void CreateCommandByArguments_UpdateArgument_CreatesUpdateCommand()
        {
            var filePath = "..\\..\\Resources\\TestEmployeeList.json";
            var service = new CreateCommandService(new List<Employee>(), filePath);

            var args = new[] { "-update", "Id:123", "FirstName:James" };
            var command = service.CreateCommandByArguments(args);

            Assert.IsInstanceOf<UpdateCommand>(command);
        }

        [Test]
        public void CreateCommandByArguments_GetArgument_CreatesGetCommand()
        {
            var filePath = "..\\..\\Resources\\TestEmployeeList.json";
            var service = new CreateCommandService(new List<Employee>(), filePath);

            var args = new[] { "-get", "Id:123" };
            var command = service.CreateCommandByArguments(args);

            Assert.IsInstanceOf<GetCommand>(command);
        }

        [Test]
        public void CreateCommandByArguments_DeleteArgument_CreatesDeleteCommand()
        {
            var filePath = "..\\..\\Resources\\TestEmployeeList.json";
            var service = new CreateCommandService(new List<Employee>(), filePath);

            var args = new[] { "-delete", "Id:123" };
            var command = service.CreateCommandByArguments(args);

            Assert.IsInstanceOf<DeleteCommand>(command);
        }

        [Test]
        public void CreateCommandByArguments_GetAllArgument_CreatesGetAllCommand()
        {
            var filePath = "..\\..\\Resources\\TestEmployeeList.json";
            var service = new CreateCommandService(new List<Employee>(), filePath);

            var args = new[] { "-getall" };
            var command = service.CreateCommandByArguments(args);

            Assert.IsInstanceOf<GetAllCommand>(command);
        }

        [Test]
        public void CreateCommandByArguments_UnknownArgument_ThrowsException()
        {
            var filePath = "..\\..\\Resources\\TestEmployeeList.json";
            var service = new CreateCommandService(new List<Employee>(), filePath);

            var args = new[] { "-unknownArgument" };

            Assert.Throws<InvalidOperationException>(() => service.CreateCommandByArguments(args));
        }
    }
}