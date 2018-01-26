using Pharmacy.Services.Interfaces;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Pharmacy.Config;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;

namespace Pharmacy.Services
{
    public class RemindersService : IRemindersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrdersService _orderService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RemindersService(IUnitOfWork unitOfWork, IOrdersService orderService)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            AutoMapperConfig.Setup();
        }

        public IEnumerable<ReminderPoco> GetCustomerReminders(Guid customerId)
        {
            logger.Info("GetCustomerReminders - customerId {0}", customerId);
            return (from r in _unitOfWork.ReminderRepository.Get()
                             join o in _unitOfWork.ReminderOrderRepository.Get() 
                                on r.ReminderId equals o.ReminderId
                where r.CustomerId == customerId
                select new ReminderPoco()
                    {
                        ReminderId = r.ReminderId,
                        SendTime = r.SendTime,
                        Drugs = _orderService.GetOrderLines(o.OrderId)
                    });
        }
        
        public IEnumerable<ReminderPoco> GetAllUnsentReminders(DateTime day)
        {
            logger.Info("GetAllUnsentReminders - {0}", day);
            var tomorrow = day.AddDays(1).AddTicks(-1); 

            return (from r in _unitOfWork.ReminderRepository.Get()
                    join o in _unitOfWork.ReminderOrderRepository.Get()
                        on r.ReminderId equals o.ReminderId
                    join c in _unitOfWork.CustomerRepository.Get()
                        on r.CustomerId equals c.CustomerId
                    where (r.SendTime >= day && r.SendTime <= tomorrow) && r.Sent == false
                    select new ReminderPoco()
                    {
                        ReminderId = r.ReminderId,
                        Email = c.Email,
                        Drugs = _orderService.GetOrderLines(o.OrderId)
                    });
        }

    }
}
