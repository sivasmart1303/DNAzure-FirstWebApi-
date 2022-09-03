using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace EmployeeTDSCalculator
{
    //Fill your code here
    public class EmployeeTDSData
    {
        public readonly string InsertRecord = "INSERT INTO tdlEmployeeTDSInfo(Employee_Id,Employee_Name,Employee_CTC,TDS_Amount)" + "Values(@Employee_Id,@Employee_Name,@Employee__CTC,@TDS_Amount)";
        public readonly string DisplayRecords = "SELECT * FROM tdlEmployeeTDSInfo";
        public bool AddEmployeeRecords(EmployeeBO employeeTdsInfo)
        {
            try
            {
                using(sqlConnection_sqlConn = new SqlConnection(DBHandler.ConnectionString))
                {
                    using (SqlCommand cmdAddEmployeeTDSRecord = new SqlCommand(InsertRecord, _sqlConn))
                    {
                        cmdAddEmployeeTDSRecord.Parameters.AddWithvalue("@Employee_Id", employeeTdsInfo.EmployeeId);
                        cmdAddEmployeeTDSRecord.parameters.AddWithValue("@Employee_Name", employeeTdsInfo.EmployeeName);
                        cmdAddEmployeeTDSRecord.parameters.AddWithValue("@Employee_CTC", employeeTdsInfo.EmployeeCTC);
                        cmdAddEmployeeTDSRecord.parameters.AddWithValue("@Employee_Amount", employeeTdsInfo.EmployeeAmount);
                    }
                }
        }
    }
}
