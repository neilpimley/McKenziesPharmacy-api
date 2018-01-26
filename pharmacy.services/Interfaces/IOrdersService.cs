using Pharmacy.Models;
using System;
using System.Collections.Generic;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IOrdersService
    {        
        Order GetCurrentOrder(Guid customerId, int status);
        OrderPoco GetOrder(Guid id);
        IEnumerable<OrderPoco> GetOrders(Guid customerId);
        OrderLine GetOrderLine(Guid id);
        IEnumerable<DrugPoco> GetOrderLines(Guid id);
        void SubmitOrder(OrderPoco order);
        OrderLine AddToOrder(OrderLine orderLine);
        void DeleteFromOrder(OrderLine orderLine);
        
    }
}
