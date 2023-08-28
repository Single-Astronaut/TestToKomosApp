namespace TestToKomosApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();

            Employee.Hired += (employee) => Console.WriteLine($"Принят на работу: {employee.FullName}");
            Employee.Fired += (employee) => Console.WriteLine($"Уволен: {employee.FullName}");

            Employee employee1 = new Employee("Иванов", "Иван", "Иванович")
            {
                BirthDate = new DateTime(1990, 5, 10),
                Gender = Gender.Male
            };
            employees.Add(employee1);

            Employee employee2 = new Employee("Петров", "Петр", "Петрович")
            {
                BirthDate = new DateTime(1985, 10, 20),
                Gender = Gender.Male
            };
            employees.Add(employee2);

            Employee employee3 = new Employee("Сидорова", "Мария", "Алексеевна")
            {
                BirthDate = new DateTime(1995, 2, 15),
                Gender = Gender.Female
            };
            employees.Add(employee3);

            Employee.HireEmployee(employee1);
            Employee.HireEmployee(employee2);
            Employee.HireEmployee(employee3);

            Employee.FireEmployee(employee2);

            Employee.SaveEmployeesByAge(employees, 30);
        }
    }
}
