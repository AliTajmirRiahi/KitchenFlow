using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Base.Core.ApiResults
{
    public class ApiResult<T>
    {
        // وضعیت موفقیت یا شکست
        public bool Success { get; set; }

        // داده برگشتی با نوع مشخص
        public T? Payload { get; set; }

        // زمان تولید پاسخ
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // اطلاعات Pagination برای لیست‌ها
        public int? TotalCount { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        //// شناسه‌ی یکتا برای دنبال کردن درخواست در لاگ‌ها
        //public string? CorrelationId { get; set; }

        // پیام‌های هشدار غیر از خطا
        public IEnumerable<string>? Warnings { get; set; }

        // سازنده‌های کمک‌کننده
        public static ApiResult<T> Ok(
            T payload,
            int? totalCount = null,
            int? page = null,
            int? pageSize = null,
            IEnumerable<string>? warnings = null)
        {
            return new ApiResult<T>
            {
                Success = true,
                Payload = payload,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Warnings = warnings
            };
        }
    }
}
