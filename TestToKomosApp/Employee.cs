using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestToKomosApp
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Employee
    {
        private string lastName;
        private string firstName;
        private string middleName;
        private DateTime birthDate;
        private Gender gender;

        public Employee(string lastName, string firstName = null, string middlename = null)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException("Фамилия не может быть пустой или NULL");
            this.lastName = lastName;
            this.firstName = firstName;
            this.middleName = middlename;
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Фамилия не может быть пустой или NULL");
                lastName = value;
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public int Age
        {
            get { return CalculateAge(BirthDate); }
        }

        public Gender Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string FullName
        {
            get { return $"{lastName} {(!string.IsNullOrEmpty(firstName) ? firstName[0] + "." : "")} {(!string.IsNullOrEmpty(middleName) ? middleName[0] + "." : "")}"; }
        }

        public static int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
                age--;

            return age;
        }

        public void DisplayInfo (bool includeAge = false)
        {
            Console.WriteLine($"ФМО: {FullName}");
            Console.WriteLine($"Дата рождения: {BirthDate.ToShortDateString()}");
            Console.WriteLine($"Пол: {Gender}");
            if (includeAge)
                Console.WriteLine($"Возраст: {Age}");
        }

        public static void SaveEmployeesByAge(List<Employee> employees, int targetAge)
        {
            string fileName = $"возраст_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                foreach (var employee in employees)
                {
                    if (employee.Age == targetAge)
                    {
                        file.WriteLine(employee.FullName);
                    }
                }
            }
        }

        public static event Action<Employee> Hired;
        public static event Action<Employee> Fired;

        public static void HireEmployee(Employee employee)
        {
            if (employee.Age >= 18)
            {
                Hired?.Invoke(employee);
            }
            else
            {
                Console.WriteLine($"Невозможно принять {employee.FullName} на работу, так как ему меньше 18 лет.");
            }
        }

        public static void FireEmployee(Employee employee)
        {
            Fired?.Invoke(employee);
        }
    }
}
