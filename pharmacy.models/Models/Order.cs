using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderLine = new HashSet<OrderLine>();
            OrderStatusNavigation = new HashSet<OrderStatus>();
            ReminderOrder = new HashSet<ReminderOrder>();
        }

        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid? NoteId { get; set; }
        public Guid? CustomerId { get; set; }
        public int OrderStatus { get; set; }

        public Customer Customer { get; set; }
        public Note Note { get; set; }
        public ICollection<OrderLine> OrderLine { get; set; }
        public ICollection<OrderStatus> OrderStatusNavigation { get; set; }
        public ICollection<ReminderOrder> ReminderOrder { get; set; }
    }
}
