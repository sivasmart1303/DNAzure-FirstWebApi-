using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace EmployeeTDSCalculator
{
    //Do not change any code here 

    public class Program
    {
        public static void Main(string[] args)
        {
            EmployeeTDSInfo employeeData = new EmployeeTDSInfo();
            EmployeeTDSData employeeTDSData = new EmployeeTDSData();
            SalaryValidator salaryValidator = new SalaryValidator();
            TDSCalculation tdsCalculation = new TDSCalculation();

            string loopInput = string.Empty;
            List<EmployeeTDSInfo> employeeTdsList = new List<EmployeeTDSInfo>();
            
            Console.WriteLine("Welcome Admin to Employee TDS Calculator Application");
            do
            {
                Console.WriteLine("\nMenu:\nPress 1 to enter new record\n" +
                    "Press 2 to display record");
                Console.WriteLine("Enter your choice:");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        try
                        {
                            bool employeeIdPatternCheck = false;
                            bool employeeIdAlreadyExistsCheck = false;
                            bool employeeIdIsEmptyCheck = false;
                            do
                            {
                                string employeeId = string.Empty;
                                Console.WriteLine("\nEnter Employee Id[Starts with EMP and next 5 will be digits]: ");
                                try
                                {
                                    employeeId = Console.ReadLine();

                                    if (string.IsNullOrEmpty(employeeId) || employeeId == "\u0020")
                                    {
                                        throw new ArgumentNullException("Employee ID can not be empty");
                                    }
                                    else
                                    {
                                        employeeIdIsEmptyCheck = false;
                                    }

                                    bool check = Regex.IsMatch(employeeId, @"^[EMP]{3}[0-9]{5}$");
                                    if (check == true)
                                    {
                                        employeeIdPatternCheck = false;

                                    }
                                    List<EmployeeTDSInfo> listData = employeeTDSData.DisplayAllEmployeeTDSRecords();
                                    var data = listData.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                                    if (data != null)
                                    {
                                        Console.WriteLine("Employee Id already exists..Try another Id");
                                        employeeIdAlreadyExistsCheck = true;
                                    }
                                    else
                                    {
                                        employeeIdAlreadyExistsCheck = false;
                                    }                                   

                                    employeeData.EmployeeId = employeeId;
                                }
                                catch (FormatException fe)
                                {
                                    Console.WriteLine(fe.Message);
                                    employeeIdPatternCheck = true;
                                }
                                catch (ArgumentNullException ae)
                                {
                                    Console.WriteLine(ae.Message);
                                    employeeIdIsEmptyCheck = true;
                                }
                            }
                            while (employeeIdPatternCheck == true || employeeIdAlreadyExistsCheck == true || employeeIdIsEmptyCheck == true);

                            bool nullEmployeeNameCheck = false;
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Enter Employee Name:");
                                    string employeeName = Console.ReadLine();
                                    if (string.IsNullOrEmpty(employeeName) || employeeName == "\u0020")
                                    {
                                        throw new ArgumentNullException("Employee Name can not be empty");
                                    }
                                    else
                                    {
                                        nullEmployeeNameCheck = false;
                                    }
                                    employeeData.EmployeeName = employeeName;
                                }
                                catch (ArgumentNullException ar)
                                {
                                    Console.WriteLine(ar.Message);
                                    nullEmployeeNameCheck = true;
                                }
                            }
                            while (nullEmployeeNameCheck == true);

                            string ctcValidator = null;
                            bool salaryEmptyCheck = false;
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Enter Employee CTC:");
                                    double salary = Convert.ToDouble(Console.ReadLine());

                                    if (string.IsNullOrEmpty(salary.ToString()) || salary.ToString() == "\u0020")
                                    {
                                        throw new FormatException();
                                    }
                                    else
                                    {
                                        salaryEmptyCheck = false;
                                        ctcValidator = salaryValidator.ValidateSalary(salary);
                                        if (ctcValidator != null)
                                        {
                                            Console.WriteLine(ctcValidator);
                                        }
                                        else
                                        {
                                            employeeData.EmployeeCTC = salary;
                                        }
                                    }
                                }
                                catch (FormatException ar)
                                {
                                    Console.WriteLine("Employee Salary can not be Blank");
                                    salaryEmptyCheck = true;
                                }
                            }
                            while (ctcValidator != null || salaryEmptyCheck == true);

                           tdsCalculation.CalculateTDS(employeeData);
                            int insertResult = employeeTDSData.InsertEmployeeTDSRecord(employeeData);
                            if (insertResult > 0)
                            {
                                Console.WriteLine("Employee TDS Records Successfully Inserted");
                                employeeTdsList.Add(employeeData);
                            }
                        }

                        catch
                        {
                            Console.WriteLine("Data Insertion Failed. Either Employee Id already exists or wrong application logic");
                        }
                        break;

                    case 2:
                        Console.WriteLine("\nDisplay All Employees TDS Records");
                        Console.WriteLine("{0,-20}{1,-20}{2,-20}{3}", "Employee Id", "Employee Name", "Employee CTC",
                            "TDS Amount");
                        List<EmployeeTDSInfo> displayRecords = employeeTDSData.DisplayAllEmployeeTDSRecords();
                        if (displayRecords.Count > 0)
                        {
                            foreach (var data in displayRecords)
                            {
                                Console.WriteLine("{0,-20}{1,-20}{2,-20}{3}", data.EmployeeId,
                                    data.EmployeeName, data.EmployeeCTC, data.TdsAmount);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Employee Information present under EmployeeTDS Table");
                        }
                        break;
                }
                Console.WriteLine("\nPress YES to repeat Menu...Any other key to stop");
                loopInput = Console.ReadLine();
            }
            while (loopInput.Equals("yes", StringComparison.InvariantCultureIgnoreCase));

            Console.WriteLine("\nThank you for using the application. Have a nice day");

        }
    }
}
