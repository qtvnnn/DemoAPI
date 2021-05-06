using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public bool CheckCustomerCode(string customerCode)
        {
            var sqlCheckExistCode = $"select CustomerCode from Customer where CustomerCode = {customerCode}";
            var customerExistCode = _dbConnection.Query<string>(sqlCheckExistCode);
            if (customerExistCode.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
