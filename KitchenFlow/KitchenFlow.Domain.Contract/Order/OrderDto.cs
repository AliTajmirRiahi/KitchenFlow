using Arta.Domain.Core.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Contract.Order
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }

        public Enums.OrderStatus OrderStatus { get; set; }

        public string OrderStatusDescription { get; set; } = string.Empty;

        public List<OrderItemDto> Items { get; set; } = new();
    }
}
