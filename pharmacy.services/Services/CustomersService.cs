using System;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using Pharmacy.Config;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;
using Pharmacy.Services.Interfaces;
using Pharmacy.Models;

namespace Pharmacy.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CustomersService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            AutoMapperConfig.Setup();
        }

        public async Task<CustomerPoco> GetCustomerByUsername(string username)
        {
            logger.Info("GetCustomerByUsername - {0}", username);

            var _customers = await _unitOfWork.CustomerRepository
                .Get(c => c.UserId == username);

            // Wait on a single task with no timeout specified.
            Task getCustomerTask = Task.Factory.StartNew(() => _unitOfWork.CustomerRepository
                .Get(c => c.UserId == username));
            getCustomerTask.Wait();
            
            if (getCustomerTask.IsCompleted) {
                var _customer = _customers.FirstOrDefault();
                if (_customer == null)
                {
                    logger.Error("GetCustomerByUsername - User doesn't exist");
                    return null;
                }
                var customer = Mapper.Map<CustomerPoco>(_customer);
                customer.Title = Mapper.Map<Title>(await _unitOfWork.TitleRepository.GetByID(_customer.TitleId));
                customer.Address = Mapper.Map<Address>(await _unitOfWork.AddressRepository.GetByID(_customer.AddressId));
                customer.Shop = Mapper.Map<Shop>(await _unitOfWork.ShopRepository.GetByID(_customer.ShopId));
                customer.Doctor = Mapper.Map<Doctor>(await _unitOfWork.DoctorRepository.GetByID(_customer.DoctorId));
            }
            return customer;
        }

        public async Task<CustomerPoco> GetCustomer(Guid id) {
            logger.Info("GetCustomer - {0}", id);
            var _customer = await _unitOfWork.CustomerRepository.GetByID(id);
            if (_customer == null)
            {
                logger.Error("GetCustomerByUsername - User doesn't exist");
                return null;
            }
            var customer = Mapper.Map<CustomerPoco>(_customer);
            customer.Title = Mapper.Map<Title>(await _unitOfWork.TitleRepository.GetByID(_customer.TitleId));
            customer.Address = Mapper.Map<Address>(await _unitOfWork.AddressRepository.GetByID(_customer.AddressId));
            customer.Shop = Mapper.Map<Shop>(await _unitOfWork.ShopRepository.GetByID(_customer.ShopId));
            customer.Doctor = Mapper.Map<Doctor>(await _unitOfWork.DoctorRepository.GetByID(_customer.DoctorId));
            return customer;
        }

        public async Task<CustomerPoco> RegisterCustomer(CustomerPoco customer)
        {
            logger.Info("RegisterCustomer - {0}", customer.Fullname);
            customer.CustomerId = Guid.NewGuid();
            customer.CreatedOn = DateTime.Now;
            customer.AddressId = Guid.NewGuid();
            customer.Address.AddressId = customer.AddressId;
            customer.Address.CreatedOn = DateTime.Now;

            var _customer = Mapper.Map<Customer>(customer);
            _unitOfWork.CustomerRepository.Insert(_customer);
            _unitOfWork.AddressRepository.Insert(customer.Address);
            try
            {
                _unitOfWork.Save();
                customer.Title = Mapper.Map<Title>(await _unitOfWork.TitleRepository.GetByID(_customer.TitleId));
                customer.Shop = Mapper.Map<Shop>(await _unitOfWork.ShopRepository.GetByID(_customer.ShopId));
                customer.Doctor = Mapper.Map<Doctor>(await _unitOfWork.DoctorRepository.GetByID(_customer.DoctorId));
            }
            catch (Exception ex)
            {
                logger.Error("RegisterCustomer - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public void UpdateCustomerDetails(CustomerPoco customer) {
            logger.Info("UpdateCustomerDetails - {0}", customer.CustomerId);
            var _customer = Mapper.Map<Customer>(customer);
            _unitOfWork.CustomerRepository.Update(_customer);
            _unitOfWork.AddressRepository.Update(customer.Address);
            try
            {
                _unitOfWork.Save();
                var task = _emailService.SendPersonalDetailsAmended(customer);
            }
            catch (Exception ex)
            {
                logger.Error("UpdateCustomerDetails - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void ActivateCustomer(Guid id, string mobileVerificationCode)
        {
            logger.Info("ActiveateCustomer - {0}", id);
            // Wait on a single task with no timeout specified.
            Task getCustomerTask = Task.Factory.StartNew(() => _unitOfWork.CustomerRepository
                .Get(c => c.CustomerId == id));
            getCustomerTask.Wait();

            if (getCustomerTask.IsCompleted)
            {


                var customer = _unitOfWork.CustomerRepository
                .Get(x => x.CustomerId == id).FirstOrDefault();
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            //TODO: check mobileVerificationCode against value to be stored in DB

            customer.Active = true;
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                logger.Error("ActivateCustomer - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
    
}