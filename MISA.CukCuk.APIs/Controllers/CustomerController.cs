using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.api.Model;
using MISA.CukCuk.APIs.Controllers;
using MISA.CukCuk.APIs.Extensions;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.api.Controller
{
    public class CustomerController : BaseEntityController<Customer>
    {

        [HttpPut("{customerId}")]
        public IActionResult Put(Customer customer, Guid customerId)
        {
            //Validate dữ liệu:
            //- check trùng mã:           

            //Kiểm tra xem có khách hàng nào có mã tương tự không
            var sqlCheckExistCode = $"SELECT CustomerCode FROM Customer WHERE CustomerCode = {customer.CustomerCode} AND CustomerId <> {customerId}";
            var customerExistCode = _dbConnection.Query<string>(sqlCheckExistCode);
            if (customerExistCode.Count() > 0)
            {
                return BadRequest("Mã khách hàng không được phép trùng");
            }
            customer.CustomerId = customerId;
            //Thực hiện lấy dữ liệu từ DB
            var storeName = "Proc_UpdateCustomer";
            var storeParam = customer;
            var row = _dbConnection.Execute(storeName, param: storeParam, commandType: CommandType.StoredProcedure);

            if (row == 0)
            {
                return StatusCode(204, customer);
            }
            else
            {
                return StatusCode(200, customer);
            }
        }

        [HttpDelete("{customerId}")]
        public IActionResult Delete(Guid customerId)
        {

            //Thực hiện xóa dữ liệu từ DB
            var storeName = "Proc_DeleteCustomer";
            var storeParam = new
            {
                CustomerId = customerId
            };
            var row = _dbConnection.Execute(storeName, param: storeParam, commandType: CommandType.StoredProcedure);

            if (row == 0)
            {
                return StatusCode(204, row);
            }
            else
            {
                return StatusCode(200, row);
            }
        }

        protected override void ValidateData(Customer customer)
        {
            base.ValidateData(customer);
            //Check mã khách hàng đã nhập hay chưa
            CustomerValidate.CheckCustomerCodeEmpty(customer.CustomerCode);
            //kiểm tra xem có khách hàng nào có mã tương tự không
            CustomerValidate.CheckDuplicateCustomerCode(customer.CustomerCode);
        }
    }
}
