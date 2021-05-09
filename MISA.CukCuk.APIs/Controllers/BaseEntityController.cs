using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces;
using MISA.CukCuk.APIs.Result;
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
        IBaseService<T> _baseService;

        public BaseEntityController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Toàn bộ dữ liệu: nếu có dữ liệu 200, không có dữ liệu 204</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var res = new ResponeResult();
            try
            {
                res.Data = _baseService.Get();
            }
            catch (Exception ex)
            {
                res.OnException(res, ex);
            }
            return StatusCode(200, res);
        }


        /// <summary>
        /// lấy dữ liệu theo khóa chính
        /// </summary>
        /// <param name="id">Id của bảng dữ liệu</param>
        /// <returns>Thông tin của 1 đối tượng</returns>
        /// CreatedBy: NNNang (04/05/2021)
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var res = new ResponeResult();
            try
            {
                Guid.TryParse(id, out Guid entityId);
                if (entityId != null && entityId != Guid.Empty)
                {
                    res.Data = _baseService.GetById(entityId);
                }
                else
                {
                    res.OnBadRequest(res);
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                res.OnException(res, ex);
                return StatusCode(500, res);
            }
            return StatusCode(200, res);
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
            var res = new ResponeResult();
            try
            {
                res = _baseService.Insert(entity);
            }
            catch (Exception ex)
            {
                res.OnException(res, ex);
                return StatusCode(500, res);
            }

            return StatusCode(201, res);
        }

        [HttpPut("{id}")]
        public IActionResult Put(T entity, Guid id)
        {
            var row = _baseService.Update(entity, id);
            if (row == 0)
            {
                return StatusCode(204, entity);
            }
            else
            {
                return StatusCode(200, entity);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var res = new ResponeResult();
            try
            {
                Guid.TryParse(id, out Guid entityId);
                if (entityId != null && entityId != Guid.Empty)
                {
                    var row = _baseService.Delete(id);
                }
                else
                {
                    res.OnBadRequest(res);
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                res.OnException(res, ex);
                return StatusCode(500, res);
            }

            return StatusCode(200, res);
        }

    }
}
