using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces;
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
        public CustomerController(IBaseService<Customer> baseService) : base(baseService)
        {

        }      

    }
}
