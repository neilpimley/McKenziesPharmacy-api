using System;
using System.Threading.Tasks;
using NLog;
using Pharmacy.Models;
using Pharmacy.Services.Interfaces;

namespace Pharmacy.notifications
{
    public class Functions
    {
        private readonly IRemindersService _remindersService;
        private readonly IOrdersService _ordersService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Functions(IRemindersService remindersService, IOrdersService ordersService, 
            IEmailService emailService, ISmsService smsService)
        {
            _remindersService = remindersService;
            _ordersService = ordersService;
            _emailService = emailService;
            _smsService = smsService;
        }

        public Task NotifyOrderReminder(string message)
        {
            logger.Trace("Start NotifyOrderReminder");
            Order order; 
            var reminders = _remindersService.GetAllUnsentReminders(DateTime.Now);
            foreach( var reminder in reminders)
            {
                order = _ordersService.GetOrder(reminder.OrderId);
                _emailService.SendOrderReminder(reminder);
                _smsService.SendOrderReminder(reminder);
            }


            return Task.FromResult(true);
        }
    }
}
