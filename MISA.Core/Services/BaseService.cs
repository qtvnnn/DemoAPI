using MISA.Core.Interfaces;
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

        public int Insert(T entity)
        {
            //Validate nghiệp vụ
            Validate();
            //thực hiện nghiệp vụ
            return _baseRepository.Insert(entity);
        }

        public int Update(T entity, Guid entityId)
        {
            throw new NotImplementedException();
        }
        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public virtual void Validate()
        {
             
        }
    }
}
