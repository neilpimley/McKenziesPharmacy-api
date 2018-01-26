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

                cfg.CreateMap<Customer, CustomerPoco>();
                cfg.CreateMap<Order, OrderPoco>();
                cfg.CreateMap<Title, Title>();
                cfg.CreateMap<Address, Address>();
                cfg.CreateMap<Doctor, Doctor>();
                cfg.CreateMap<Practice, Practice>();
                cfg.CreateMap<Shop, Shop>();
                cfg.CreateMap<Address, Address>();
                cfg.CreateMap<Favourite, Favourite>();
                cfg.CreateMap<Order, Order>();
                cfg.CreateMap<OrderLine, OrderLine>();
                cfg.CreateMap<OrderStatus, OrderStatus>();
            });
        }
    }
}
