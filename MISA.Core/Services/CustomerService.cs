using MISA.Core.Entities;
using MISA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        public CustomerService(IBaseRepository<Customer> baseRepository) : base(baseRepository)
        {
        }

        public override void Validate()
        {
            //var isDuplicate = 
            base.Validate();
        }
    }
}
