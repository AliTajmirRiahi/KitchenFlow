using Arta.Base.Core.Exceptions;
using Arta.Domain.Core.Commons;
using Arta.Domain.Core.Commons.Enums;
using Arta.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Arta.Domain.Core.Commons.Enums.Enums;

namespace KitchenFlow.Domain.Order
{
    public class Order : AggregateRoot<Guid>
    {
        public int CustomerId { get; private set; }
        public int TableId { get; private set; }
        public Enums.OrderStatus Status { get; private set; }

        private readonly List<OrderItem> _items = new();
        public virtual IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        [NotMapped]
        private OrderStatusStateMachine _stateMachine = default!;

        private Order()
        {
        } // For EF

        public Order(int customerId, int tableId)
        {
            if (customerId <= 0)
                throw new DomainValidationException("CustomerId is invalid.", "ORDER_INVALID_CustomerId", System.Net.HttpStatusCode.UnprocessableEntity);

            IsValidTable(tableId);

            Id = Guid.NewGuid();
            CustomerId = customerId;
            TableId = tableId;
            Status = OrderStatus.Created;

            InitializeStateMachine(OrderStatus.Created);
        }


        public void AddItem(int productId, int quantity, decimal unitPrice)
        {
            if (Status > OrderStatus.Confirmed)
                throw new BaseException($"Can not add new item to Order with id {this.Id}.", "OrderIsImmutable", System.Net.HttpStatusCode.BadRequest);

            _items.Add(new OrderItem(productId, quantity, unitPrice));
        }
        public void Validate()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Order must have at least one item.");
        }
        public decimal GetTotalAmount() => _items.Sum(i => i.Total);

        public void InitializeStateMachine(OrderStatus orderStatus)
        {
            _stateMachine = new OrderStatusStateMachine(orderStatus);
        }



        public void Accept() => Fire(OrderTrigger.Accept);
        public void StartPreparation() => Fire(OrderTrigger.StartPrep);
        public void FinishPreparation() => Fire(OrderTrigger.FinishPrep);
        public void Serve() => Fire(OrderTrigger.Serve);
        public void Close() => Fire(OrderTrigger.Close);
        public void Cancel() => Fire(OrderTrigger.Cancel);

        public void GoNextState()
        {
            if (_stateMachine == null)
                InitializeStateMachine(this.Status);

            var permittedTriggers = _stateMachine!.Machine.PermittedTriggersAsync.Result.ToList();

            if (permittedTriggers.Count == 0)
                throw new InvalidOperationException("No next state available.");

            var nextTrigger = permittedTriggers.First();
            _stateMachine.Machine.Fire(nextTrigger);

            Status = _stateMachine.Machine.State;
        }

        public void ChangeTable(int tableId)
        {
            IsValidTable(tableId);

            this.TableId = tableId;
        }


        #region Private Methods

        private static void IsValidTable(int tableId)
        {
            if (tableId <= 0)
                throw new DomainValidationException("TableId must be greater than zero.", "ORDER_INVALID_tableId", System.Net.HttpStatusCode.UnprocessableEntity);
        }

        private void Fire(OrderTrigger trigger)
        {
            try
            {
                if (_stateMachine == null)
                    InitializeStateMachine(this.Status);

                _stateMachine!.Machine.Fire(trigger);
                Status = _stateMachine.Machine.State;
            }
            catch (InvalidOperationException)
            {
                // Converting a system error into a business error that is understandable to the client
                throw new BaseException(
                    $"Cannot perform '{trigger}' on an order that is currently '{Status}'.",
                    "InvalidStateTransition",
                    HttpStatusCode.BadRequest);
            }
        }

        #endregion
    }
}
