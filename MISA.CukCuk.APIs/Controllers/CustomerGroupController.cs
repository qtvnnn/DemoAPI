using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.APIs.Controllers
{
    public class CustomerGroupController : BaseEntityController<CustomerGroup>
    {
        public CustomerGroupController(IBaseService<CustomerGroup> baseService) : base(baseService)
        {
        }

        [HttpPut("{customerId}")]
        public IActionResult Put(CustomerGroup customerGroup, Guid customerId)
        {
            return StatusCode(200);
        }

        [HttpDelete("{customerId}")]
        public IActionResult Delete(Guid customerId)
        {
            return StatusCode(200);
        }
    }
}
