using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.APIs.Extensions
{
    public class CustomerValidate
    {
        /// <summary>
        /// Check mã khách hàng bị để trống
        /// </summary>
        /// <param name="customerCode">Mã khách hàng</param>
        /// CreatedBy: NVManh
        public static void CheckCustomerCodeEmpty(string customerCode)
        {
            if (string.IsNullOrEmpty(customerCode))
            {
                throw new ValidateExceptions("Mã khách hàng không được phép để trống");
            }
        }

        public static void CheckDuplicateCustomerCode(string customerCode)
        {
            string _connectionString = "Host=47.241.69.179; Port=3306; User Id= dev; Password=12345678; Database= MF0_NVManh_CukCuk02";
            IDbConnection _dbConnection = new MySqlConnection(_connectionString);
            var sqlCheckExistCode = $"select CustomerCode from Customer where CustomerCode = {customerCode}";
            var customerExistCode = _dbConnection.Query<string>(sqlCheckExistCode);
            if (customerExistCode.Count() > 0)
            {
                throw new ValidateExceptions("mã khách hàng không được phép trùng");
            }
        }
    }
}
