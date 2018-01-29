using AutoMapper;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Config
{
    public static class AutoMapperConfig
    {
        public static void Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CustomerPoco, Customer>();
                cfg.CreateMap<OrderPoco, Order>();
                cfg.CreateMap<DrugPoco, Drug>();
                cfg.CreateMap<ReminderPoco, Reminder>();

                cfg.CreateMap<Customer, CustomerPoco>();
                cfg.CreateMap<Order, OrderPoco>();
                cfg.CreateMap<Drug, DrugPoco>();
                cfg.CreateMap<Reminder, ReminderPoco>();
            });
        }
    }
}
