using MISA.CukCuk.APIs.Enumeration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.APIs.Result
{
    public class ResponeResult
    {
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Thông báo lỗi cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; } = ErrorCode.NONE;

        /// <summary>
        /// Thông tin tìm hiểu thêm cho dev
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// Mã lỗi để tìm kiếm trên trang nào đó
        /// </summary>
        public string TradeId { get; set; }

        /// <summary>
        /// Dữ liệu của đối tượng trả về
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Xử lí lỗi
        /// </summary>
        /// <param name="result">Kết quả trả về lỗi</param>
        /// <param name="ex">lỗi văng ra</param>
        public void OnException(ResponeResult result, Exception ex)
        {
            //to do
            result.UserMsg = Core.Resource.Message.ExceptionUser;
        }

        public void OnBadRequest(ResponeResult result)
        {
            result.IsSuccess = false;
            result.ErrorCode = ErrorCode.ERRROR_DATA;
            result.DevMsg = Core.Resource.Message.ErrorData;
            result.UserMsg = Core.Resource.Message.ExceptionUser;
        }

        public void ValidateException(ResponeResult result, string message)
        {
            result.IsSuccess = false;
            result.ErrorCode = ErrorCode.ERRROR_DATA;
            result.DevMsg = Core.Resource.Message.ErrorData;
            result.UserMsg = message;
        }
    }
}
