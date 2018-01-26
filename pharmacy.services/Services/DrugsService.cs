using Pharmacy.Services.Interfaces;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;

namespace Pharmacy.Services
{
    public class DrugsService : IDrugsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DrugsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DrugPoco> GetDrugs(Guid customerId, string drugName)
        {
            logger.Info("GetDrugs - {0}, {1}", customerId, drugName);
            return (from d in _unitOfWork.DrugRepository.Get(filter: d => d.DrugName.StartsWith(drugName))
                    join f in _unitOfWork.FavouriteRepository.Get(f => f.CustomerId == customerId) 
                    on d.DrugId equals f.DrugId into favs
                    from f in favs.DefaultIfEmpty()
                    orderby d.DrugName
                    select new DrugPoco
                    {
                        DrugId = d.DrugId,
                        DrugName = d.DrugName,
                        IsFavourite = f == null ? false : true
                    });
        }
    }
}