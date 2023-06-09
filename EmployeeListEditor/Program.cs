namespace EmployeeListEditor
{
    internal class Program
    {
        private const string JsonPath = "..\\..\\Resources\\EmployeeList.json";

        static void Main(string[] args)
        {
            var openFileService = new ReadFileService();
            var employes = openFileService.ReadByFilePath(JsonPath);

            var createCommandService = new CreateCommandService(employes, JsonPath);
            var command = createCommandService.CreateCommandByArguments(args);
            command.Execute();
        }
    }
}
