using Pharmacy.Models;
using System;
using System.Collections.Generic;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IRemindersService
    {
        IEnumerable<ReminderPoco> GetCustomerReminders(Guid customerId);
        IEnumerable<ReminderPoco> GetAllUnsentReminders(DateTime day);
    }
}
