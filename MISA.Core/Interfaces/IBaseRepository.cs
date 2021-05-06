using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces
{
    /// <summary>
    /// Base Respository
    /// </summary>
    /// <typeparam name="T">Kiểu của đối tượng</typeparam>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu trong db
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: NNNANG (06/05/21)
        IEnumerable<T> Get();
        /// <summary>
        /// Lấy thông tin một đối tượng theo id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>đối tượng có id tương ứng</returns>
        /// CreatedBy: NNNANG (06/05/21)
        T GetById(Guid entityId);
        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);
        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int Update(T entity, Guid entityId);
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        int Delete(Guid entityId);
    }
}
