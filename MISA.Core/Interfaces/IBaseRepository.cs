using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu trong db
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: NNNANG (06/05/21)
        IReadOnlyList<T> Get();
        /// <summary>
        /// Lấy thông tin một đối tượng theo id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>đối tượng có id tương ứng</returns>
        T GetById(Guid entityId);
        int Insert(T entity);
        int Update(T entity, Guid entityId);
    }
}
