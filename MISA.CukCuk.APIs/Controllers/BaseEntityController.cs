using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.APIs.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public abstract class BaseEntityController<T> : ControllerBase
    {
        protected string _tableName = string.Empty;
        protected string _connectionString = "Host=47.241.69.179; Port=3306; User Id= dev; Password=12345678; Database= MF0_NVManh_CukCuk02";
        protected IDbConnection _dbConnection;
        IBaseService<T> _baseService;

        public BaseEntityController(IBaseService<T> baseService)
        {
            _baseService = baseService;
            _tableName = typeof(T).Name;
            _dbConnection = new MySqlConnection(_connectionString);
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Toàn bộ dữ liệu: nếu có dữ liệu 200, không có dữ liệu 204</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.Get();
            if (entities.Count() == 0)
            {
                return StatusCode(204, entities);
            }
            else
            {
                return StatusCode(200, entities);
            }
        }


        /// <summary>
        /// lấy dữ liệu theo khóa chính
        /// </summary>
        /// <param name="CustomerId">Id của bảng dữ liệu</param>
        /// <returns>Thông tin của 1 đối tượng</returns>
        /// CreatedBy: NNNang (04/05/2021)
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            //Thực hiện lấy dữ liệu từ DB
            var storeName = $"Proc_Get{_tableName}ById";

            DynamicParameters dynamicParameters = new DynamicParameters();
            var storeCustomerId = $"@{_tableName}Id";
            dynamicParameters.Add(storeCustomerId, id);

            var customer = _dbConnection.Query<T>(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            if (customer == null)
            {
                return StatusCode(204, customer);
            }
            else
            {
                return StatusCode(200, customer);
            }
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Bản ghi muốn thêm mới</param>
        /// <returns>
        ///     200: thành công thêm dữ liệu
        ///     400: bad request, lỗi dữ liệu không hợp lệ
        ///     500: nếu có lỗi hoặc Exception trên server
        /// </returns>
        [HttpPost]
        public IActionResult Post(T entity)
        {
            //Validate dữ liệu
            ValidateData(entity);
            //Thực hiện lấy dữ liệu từ DB
            var storeName = $"Proc_Insert{_tableName}";
            var storeParam = entity;
            var row = _dbConnection.Execute(storeName, param: storeParam, commandType: CommandType.StoredProcedure);

            if (row == 0)
            {
                return StatusCode(204, entity);
            }
            else
            {
                return StatusCode(200, entity);
            }
        }

        protected virtual void ValidateData(T entity)
        {

        }
    }
}
