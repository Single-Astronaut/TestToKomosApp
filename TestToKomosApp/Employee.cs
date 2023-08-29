using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestToKomosApp
{
    /// <summary>
    /// Класс "Сотрудник".
    /// </summary>
    public class Employee
    {
        private string lastName;
        private string firstName;
        private string middleName;
        private DateTime birthDate;
        private Gender gender;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="lastName"> Фамилия. </param>
        /// <param name="firstName"> Имя. </param>
        /// <param name="middlename"> Отчество. </param>
        /// <exception cref="ArgumentNullException"></exception>
        public Employee(string lastName, string firstName = null, string middlename = null)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException("Фамилия не может быть пустой или NULL");
            this.lastName = lastName;
            this.firstName = firstName;
            this.middleName = middlename;
        }

        /// <summary>
        /// Фамилия.
        /// </summary>
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

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age
        {
            get { return CalculateAge(BirthDate); }
        }

        /// <summary>
        /// Пол.
        /// </summary>
        public Gender EmployeeGender
        {
            get { return gender; }
            set { gender = value; }
        }

        /// <summary>
        /// Перечисляемый тип "Пол".
        /// </summary>
        public enum Gender
        {
            Male,
            Female
        }

        /// <summary>
        /// ФИО.
        /// </summary>
        public string FullName
        {
            get { return $"{lastName} {(!string.IsNullOrEmpty(firstName) ? firstName[0] + "." : "")} {(!string.IsNullOrEmpty(middleName) ? middleName[0] + "." : "")}"; }
        }

        /// <summary>
        /// Вычисление возраста.
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public static int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
                age--;

            return age;
        }

        /// <summary>
        /// Отображение информации о сотруднике в сообщении.
        /// </summary>
        /// <param name="includeAge"></param>
        public void DisplayInfo (bool includeAge = false)
        {
            Console.WriteLine($"ФМО: {FullName}");
            Console.WriteLine($"Дата рождения: {BirthDate.ToShortDateString()}");
            Console.WriteLine($"Пол: {EmployeeGender}");
            if (includeAge)
                Console.WriteLine($"Возраст: {Age}");
        }

        /// <summary>
        /// Сохранение в файл сотрудников с указанным возрастом.
        /// </summary>
        /// <param name="employees"></param>
        /// <param name="targetAge"></param>
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

        /// <summary>
        /// Принять на работу.
        /// </summary>
        /// <param name="employee"></param>
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

        /// <summary>
        /// Уволить.
        /// </summary>
        /// <param name="employee"></param>
        public static void FireEmployee(Employee employee)
        {
            Fired?.Invoke(employee);
        }
    }
}
