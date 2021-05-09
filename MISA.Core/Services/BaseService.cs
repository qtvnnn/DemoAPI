using MISA.Core.Interfaces;
using MISA.CukCuk.APIs.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        IBaseRepository<T> _baseRepository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public IEnumerable<T> Get()
        {
            var entities = _baseRepository.Get();
            return entities;
        }

        public T GetById(Guid entityId)
        {
            return _baseRepository.GetById(entityId);
        }

        public ResponeResult Insert(T entity)
        {
            ValidateObject(entity);
            return _baseRepository.Insert(entity);
        }

        public int Update(T entity, Guid entityId)
        {            
            return _baseRepository.Update(entity, entityId);
        }
        public int Delete(string entityId)
        {
            return _baseRepository.Delete(entityId);
        }

        private void ValidateObject(T entity)
        {
            // Validate với các trường thông tin bắt buộc nhập:

            // Gọi đến hàm validate tùy chọn:
            ValidateCustom(entity);
        }

        protected virtual void ValidateCustom(T entity)
        {

        }
    }
}
