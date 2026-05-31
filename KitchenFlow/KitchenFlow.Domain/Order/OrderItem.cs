using Arta.Domain.Core.Commons;
using Arta.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenFlow.Domain.Order
{
    public class OrderItem : Entity<int>
    {
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Total { get; internal set; }

        public OrderItem() { }

        public OrderItem( int productId, int quantity, decimal unitPrice)
        {
            if (productId <= 0)
                throw new DomainValidationException("ProductId is invalid.", "ORDER_INVALID_PRODUCT", System.Net.HttpStatusCode.UnprocessableEntity);

            if (quantity <= 0)
                throw new DomainValidationException("Quantity must be greater than zero.", "ORDER_INVALID_QUANTITY", System.Net.HttpStatusCode.UnprocessableEntity);

            if (unitPrice < 0)
                throw new DomainValidationException("Unit price cannot be negative.", "ORDER_INVALID_PRICE", System.Net.HttpStatusCode.UnprocessableEntity);

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Total = quantity * UnitPrice;
        }
    }
}
