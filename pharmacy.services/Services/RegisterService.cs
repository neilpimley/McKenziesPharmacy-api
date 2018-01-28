﻿
using Pharmacy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using getAddress.Sdk;
using getAddress.Sdk.Api.Requests;
using getAddress.Sdk.Api.Responses;
using Microsoft.Extensions.Options;
using NLog;
using Pharmacy.Config;
using Pharmacy.Exceptions;
using Pharmacy.Models;
using Pharmacy.Repositories.Interfaces;
using Twilio.Exceptions;

namespace Pharmacy.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly string _apiKey;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RegisterService(IUnitOfWork unitOfWork, IOptions<ServiceSettings> serviceSettings)
        {
            _unitOfWork = unitOfWork;
            _apiKey = serviceSettings.Value.GetAddressApiKey;
            AutoMapperConfig.Setup();
        }

        public async Task<IEnumerable<Shop>> GetShops()
        {
            try
            {
                return Mapper.Map<IEnumerable<Shop>>(await _unitOfWork.ShopRepository.Get());
            }
            catch (Exception ex)
            {
                logger.Error("GetShops - Error: {0}", ex.Message);
                throw new DataRetrieverException(typeof(Shop).Name);
            }
        }

        public async Task<IEnumerable<Title>> GetTitles()
        {
            try
            {
                return Mapper.Map<IEnumerable<Title>>(await _unitOfWork.TitleRepository.Get());
            }
            catch (Exception ex)
            {
                logger.Error("GetTitles - Error: {0}", ex.Message);
                throw new DataRetrieverException(typeof(Title).Name);
            }
        }

        public async Task<IEnumerable<Practice>> GetPractices()
        {
            try
            {
                return Mapper.Map<IEnumerable<Practice>>(await _unitOfWork.PracticeRepository.Get());
            }
            catch (Exception ex)
            {
                logger.Error("GetPractices - Error: {0}", ex.Message);
                throw new DataRetrieverException(typeof(Practice).Name);
            }
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            try
            {
            return Mapper.Map<IEnumerable<Doctor>>(await _unitOfWork.DoctorRepository.Get());
            }
            catch (Exception ex)
            {
                logger.Error("GetDoctors - Error: {0}", ex.Message);
                throw new DataRetrieverException(typeof(Doctor).Name);
            }
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByPractice(Guid practiceId)
        {
            try
            {
            return Mapper.Map<IEnumerable<Doctor>>(await _unitOfWork.DoctorRepository.Get(d => d.PracticeId == practiceId));
            }
            catch (Exception ex)
            {
                logger.Error("GetShops - Error: {0}", ex.Message);
                throw new DataRetrieverException("Shops");
            }
        }

        
        public async Task<List<Models.Address>> GetAddressesByPostcode(string postCode)
        {
            var apiKey = new ApiKey(_apiKey);
            var addresses = new List<Models.Address>();

            using (var api = new GetAddesssApi(apiKey))
            {
                try
                {
                    var result = await api.Address.Get(new GetAddressRequest(postCode));

                    if (result.IsSuccess)
                    {
                        var successfulResult = (GetAddressResponse.Success) result;

                        addresses = successfulResult.Addresses.Select(a => new Models.Address()
                        {
                            AddressLine1 = a.Line1,
                            AddressLine2 = a.Line2,
                            AddressLine3 = a.Line3,
                            Town = a.TownOrCity,
                            County = a.County,
                            Postcode = postCode
                        }).ToList();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("GetAddressesByPostcode failed for '{0}'. Error: {1}", postCode, ex.Message );
                    throw new ApiConnectionException("GetPostcodeApi");
                }
                            }
            return addresses;
        }

        

    }
}