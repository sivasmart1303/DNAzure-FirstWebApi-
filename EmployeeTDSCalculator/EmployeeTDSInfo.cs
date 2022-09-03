using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EmployeeTDSCalculator
{
    //Fill your code here
    public class EmployeeInfo
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double EmployeeCTC { get; set; }
        public double TdsAmount { get; set; }

        public EmployeeInfo()
        {

        }
        public EmployeeInfo(string employeeId,string employeeName,double employeeCTS,double tdsAmount)
        {
            EmployeeId = EmployeeId;
            EmployeeName = EmployeeName;
            EmployeeCTC = EmployeeCTC;
            TdsAmount = tdsAmount;
        }
    }
}
