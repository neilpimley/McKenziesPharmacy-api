using System;
using System.Collections.Generic;
using Pharmacy.Models;
using Pharmacy.Models.Pocos;

namespace Pharmacy.Services.Interfaces
{
    public interface IFavouritesService
    {
        IEnumerable<DrugPoco> GetFavouriteDrugs(Guid customerId);
        Favourite GetFavourite(Guid id);
        Favourite AddFavourite(Favourite favouriteDrug);
        void DeleteFavourite(Guid id);
    }
}