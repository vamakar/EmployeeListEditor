using System.Collections.Generic;

namespace EmployeeListEditor.Commands
{
    public abstract class Command
    {
        protected ICollection<Employee> Employees;
        public abstract void Execute();
    }
}
