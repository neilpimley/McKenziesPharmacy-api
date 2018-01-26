using System;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface ICustomersService
    {
        CustomerPoco GetCustomerByUsername(string username);
        CustomerPoco GetCustomer(Guid id);
        CustomerPoco RegisterCustomer(CustomerPoco customer);
        void UpdateCustomerDetails(CustomerPoco customer);
        void ActivateCustomer(Guid id, string mobileVerificationCode);
    }
}
