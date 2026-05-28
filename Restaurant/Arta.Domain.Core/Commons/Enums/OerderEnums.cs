using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Domain.Core.Commons.Enums
{
    public static partial class Enums
    {
        public enum OrderStatus
        {
            Created = 0,       // Order is created
            Confirmed = 1,     // The Restuarant confirmed 
            InPreparation = 2, // Order is preparing
            Ready = 3,         // Order is ready to deliver
            Delivered = 4,     // Order is delivered to customer
            Completed = 5,     // Order flow completed
            Cancelled = 6      // Order is cancelled
        }

        public enum OrderTrigger
        {
            Accept,      // The Restuarant is accepting the order 
            StartPrep,   // Strarting Preparation
            FinishPrep,  // Finishing Preparation
            Serve,       // Deliver to customer
            Close,       // Finishing
            Cancel       // Cancel
        }

        /// <summary>
        /// Returns a human-readable description for the current order status.
        /// </summary>
        public static string ToDescription(this OrderStatus status)
        {
            // human-readable description for the current order status
            return status switch
            {
                OrderStatus.Created => "Order is created",
                OrderStatus.Confirmed => "The restaurant confirmed the order",
                OrderStatus.InPreparation => "Order is being prepared",
                OrderStatus.Ready => "Order is ready for delivery",
                OrderStatus.Delivered => "Order has been delivered to the customer",
                OrderStatus.Completed => "Order flow completed",
                OrderStatus.Cancelled => "Order was cancelled",
                _ => "Unknown order status"
            };
        }
    }


}
