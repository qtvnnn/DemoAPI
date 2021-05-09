using Dapper;
using MISA.Core.Interfaces;
using MISA.CukCuk.APIs.Result;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        protected string _tableName = string.Empty;
        protected string _connectionString = "Host=47.241.69.179; Port=3306; User Id= dev; Password=12345678; Database= MF0_NVManh_CukCuk02";
        protected IDbConnection _dbConnection;
        public BaseRepository()
        {
            _tableName = typeof(T).Name;
            _dbConnection = new MySqlConnection(_connectionString);
        }

        public IEnumerable<T> Get()
        {
            //Thực hiện lấy dữ liệu từ DB
            var entities = _dbConnection.Query<T>($"Proc_Get{_tableName}s", commandType: CommandType.StoredProcedure);
            return entities;
        }

        public T GetById(Guid entityId)
        {
            var storeName = $"Proc_Get{_tableName}ById";

            DynamicParameters dynamicParameters = new DynamicParameters();
            var storeEntityId = $"@{_tableName}Id";
            dynamicParameters.Add(storeEntityId, entityId);

            var entity = _dbConnection.Query<T>(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return entity;
        }

        public ResponeResult Insert(T entity)
        {
            var res = new ResponeResult();

            //Thực hiện lấy dữ liệu từ DB
            var storeName = $"Proc_Insert{_tableName}";
            var storeParam = entity;
            _dbConnection.Execute(storeName, param: storeParam, commandType: CommandType.StoredProcedure);

            return res;
        }


        // đang lỗi update
        public int Update(T entity, Guid entityId)
        {
            //customer.CustomerId = customerId;
            //Thực hiện lấy dữ liệu từ DB
            var storeName = $"Proc_Update{_tableName}";

            DynamicParameters dynamicParameters = new DynamicParameters();
            var storeEntity = $"@{_tableName}";
            var storeEntityId = $"@{_tableName}Id";

            dynamicParameters.Add(storeEntity, entity);
            dynamicParameters.Add(storeEntityId, entity);

            return _dbConnection.Execute(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        public int Delete(string entityId)
        {
            //Thực hiện xóa dữ liệu từ DB
            var storeName = $"Proc_Delete{_tableName}";

            DynamicParameters dynamicParameters = new DynamicParameters();
            var storeEntityId = $"@{_tableName}Id";
            dynamicParameters.Add(storeEntityId, entityId);
            return _dbConnection.Execute(storeName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }
    }
}
