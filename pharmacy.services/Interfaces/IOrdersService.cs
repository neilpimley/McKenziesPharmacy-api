using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IOrdersService
    {        
        Task<Order> GetCurrentOrder(Guid customerId, int status);
        Task<OrderPoco> GetOrder(Guid id);
        Task<IEnumerable<OrderPoco>> GetOrders(Guid customerId);
        Task<OrderLine> GetOrderLine(Guid id);
        Task<IEnumerable<DrugPoco>> GetOrderLines(Guid id);
        void SubmitOrder(OrderPoco order);
        Task<OrderLine> AddToOrder(OrderLine orderLine);
        void DeleteFromOrder(OrderLine orderLine);
        
    }
}
