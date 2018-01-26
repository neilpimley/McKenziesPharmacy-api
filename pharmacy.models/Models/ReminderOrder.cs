using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public partial class ReminderOrder
    {
        public Guid ReminderOrderId { get; set; }
        public Guid ReminderId { get; set; }
        public Guid OrderId { get; set; }

        public Order Order { get; set; }
        public Reminder Reminder { get; set; }
    }
}
