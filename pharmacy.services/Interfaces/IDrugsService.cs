using System;
using System.Collections.Generic;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IDrugsService
    {
        IEnumerable<DrugPoco> GetDrugs(Guid customerId, string drugName);
    }
}