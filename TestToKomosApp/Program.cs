namespace TestToKomosApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Список сотрудников.
            List<Employee> employees = new List<Employee>();

            // Вызов событий принятия и увольнения.
            Employee.Hired += (employee) => Console.WriteLine($"Принят на работу: {employee.FullName}");
            Employee.Fired += (employee) => Console.WriteLine($"Уволен: {employee.FullName}");

            //Добавление сотрудников в список.
            Employee employee1 = new Employee("Иванов", "Александр", "Сергеевич")
            {
                BirthDate = new DateTime(1990, 5, 10),
                EmployeeGender = Employee.Gender.Male
            };
            employees.Add(employee1);

            Employee employee2 = new Employee("Петров", "Илья", "Дмитриевич")
            {
                BirthDate = new DateTime(1985, 10, 20),
                EmployeeGender = Employee.Gender.Male
            };
            employees.Add(employee2);

            Employee employee3 = new Employee("Аташева", "Мария", "Алексеевна")
            {
                BirthDate = new DateTime(1995, 2, 15),
                EmployeeGender = Employee.Gender.Female
            };
            employees.Add(employee3);

            // Принять на работу.
            Employee.HireEmployee(employee1);
            Employee.HireEmployee(employee2);
            Employee.HireEmployee(employee3);

            // Уволить.
            Employee.FireEmployee(employee2);

            // Сохранить сотрудников с возрастом 30 лет
            Employee.SaveEmployeesByAge(employees, 30);
        }
    }
}
