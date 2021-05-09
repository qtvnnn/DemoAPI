using MISA.Core.Entities;
using MISA.Core.Interfaces;
using MISA.CukCuk.APIs.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public CustomerService(IBaseRepository<Customer> baseRepository) : base(baseRepository)
        {
        }

        protected override void ValidateCustom(Customer customer)
        {
            var result = new ResponeResult();

            // Check mã khách hàng đã nhập hay chưa? nếu chưa thì trả về thông tin lỗi cho client (trả về HttpCode 400 - BadRequest):
            if (string.IsNullOrEmpty(customer.CustomerCode))
                result.ValidateException(result, Core.Resource.Message.ExceptionEmpty);

            // Check mã khách hàng đã tồn tại hay chưa? nếu đã tồn tại thì đưa thông tin lỗi cho client  (trả về HttpCode 400 - BadRequest):
            var res = _customerRepository.CheckCustomerCode(customer.CustomerCode);
            if (res == true)
            {
                result.ValidateException(result, Core.Resource.Message.ExceptionDuplicate);
            }
        }
    }
}
