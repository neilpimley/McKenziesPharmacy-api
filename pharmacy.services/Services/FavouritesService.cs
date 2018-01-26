using Pharmacy.Services.Interfaces;
using Pharmacy.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using NLog;
using Pharmacy.Models.Pocos;
using Pharmacy.Repositories.Interfaces;

namespace Pharmacy.Services
{
    public class FavouritesService : IFavouritesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public FavouritesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Favourite GetFavourite(Guid id) {
            return Mapper.Map<Favourite>(_unitOfWork.FavouriteRepository.GetByID(id));
        }

        public IEnumerable<DrugPoco> GetFavouriteDrugs(Guid customerId)
        {
            logger.Info("GetFavouriteDrugs - CustomerId: {0}", customerId);
            return (from d in _unitOfWork.DrugRepository.Get()
                    join f in _unitOfWork.FavouriteRepository.Get() on d.DrugId equals f.DrugId
                    where f.CustomerId == customerId
                    select new DrugPoco()
                    {
                        DrugId = d.DrugId,
                        DrugName = d.DrugName,
                        FavouriteId = f.FavouriteId
                    });
        }

        public Favourite AddFavourite(Favourite favouriteDrug)
        {
            logger.Info("AddFavourite - CustomerId:{0}, DrugId:{1}", favouriteDrug.CustomerId, favouriteDrug.DrugId);
            var exists = _unitOfWork.FavouriteRepository
                .Get(filter: f => f.DrugId == favouriteDrug.CustomerId
                    && f.CustomerId == favouriteDrug.CustomerId).Any();
            if (exists)
            {
                throw new Exception("Favourite already exists");
            }
            else
            { 
                favouriteDrug.FavouriteId = Guid.NewGuid();
                _unitOfWork.FavouriteRepository.Insert(favouriteDrug);
                try
                {
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    logger.Error("AddFavourite - {0}", ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return favouriteDrug;
        }
        public void DeleteFavourite(Guid id)
        {
            logger.Info("DeleteFavourite - FavouritID:{0}", id);
            _unitOfWork.FavouriteRepository.Delete(id);
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                logger.Error("DeleteFavourite - {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}