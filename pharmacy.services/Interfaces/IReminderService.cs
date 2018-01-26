using Pharmacy.Models;
using System;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IReminderService
    {
        Reminder AddReminder(ReminderPoco reminder);
        void DeleteReminder(Guid id);
    }
}
