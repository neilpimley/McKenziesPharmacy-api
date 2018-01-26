using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Services.Interfaces
{
    public interface IRegisterService
    {
        IEnumerable<Shop> GetShops();
        IEnumerable<Title> GetTitles();
        IEnumerable<Practice> GetPractices();
        IEnumerable<Doctor> GetDoctors();
        IEnumerable<Doctor> GetDoctorsByPractice(Guid practiceIdD);
        Task<List<Address>> GetAddressesByPostcode(string postCode);
    }
}