using Arta.Domain.Core.Commons;
using Arta.Domain.Core.Commons.Enums;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arta.Domain.Core.Commons.Enums.Enums;

namespace Restaurant.Domain.Order
{
    public interface IOrderStatusStateMachine : IStatusStateMachine
    {
        public StateMachine<Enums.OrderStatus, Enums.OrderTrigger> Machine { get; }
    }
    public class OrderStatusStateMachine : IOrderStatusStateMachine
    {
        private readonly StateMachine<Enums.OrderStatus, Enums.OrderTrigger> _machine = default!;
        public StateMachine<Enums.OrderStatus, Enums.OrderTrigger> Machine { get => _machine; }

        public OrderStatusStateMachine(OrderStatus orderStatus)
        {
            _machine = new StateMachine<OrderStatus, OrderTrigger>(orderStatus);
            Configure();
        }

        private void Configure()
        {
            Machine.Configure(OrderStatus.Created)
                .Permit(OrderTrigger.Accept, OrderStatus.Confirmed)
                .Permit(OrderTrigger.Cancel, OrderStatus.Cancelled);

            Machine.Configure(OrderStatus.Confirmed)
                .Permit(OrderTrigger.StartPrep, OrderStatus.InPreparation)
                .Permit(OrderTrigger.Cancel, OrderStatus.Cancelled);

            Machine.Configure(OrderStatus.InPreparation)
                .Permit(OrderTrigger.FinishPrep, OrderStatus.Ready)
                .Permit(OrderTrigger.Cancel, OrderStatus.Cancelled);

            Machine.Configure(OrderStatus.Ready)
                .Permit(OrderTrigger.Serve, OrderStatus.Delivered);

            Machine.Configure(OrderStatus.Delivered)
                .Permit(OrderTrigger.Close, OrderStatus.Completed);

            Machine.Configure(OrderStatus.Cancelled)
                .Permit(OrderTrigger.Close, OrderStatus.Completed);
        }
    }
}
