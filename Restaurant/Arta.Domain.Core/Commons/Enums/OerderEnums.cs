using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Domain.Core.Commons.Enums
{
    public static partial class Enums
    {
        public enum OrderStatus
        {
            Created = 0,       // سفارش ایجاد شده
            Confirmed = 1,     // تایید رستوران
            InPreparation = 2, // در حال آماده‌سازی
            Ready = 3,         // آماده تحویل
            Delivered = 4,     // تحویل مشتری
            Completed = 5,     // بسته و تسویه شده
            Cancelled = 6      // لغو شده
        }

        public enum OrderTrigger
        {
            Accept,      // رستوران سفارش رو قبول می‌کنه
            StartPrep,   // شروع آماده‌سازی
            FinishPrep,  // آماده شدن
            Serve,       // تحویل مشتری
            Close,       // بستن سفارش
            Cancel       // لغو
        }
    }
}
