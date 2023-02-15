﻿using MarketplaceDAL.Data;
using MarketplaceDAL.UnitOfWork;

namespace Marketplace.BL.Services
{
    public class UnitOfWorkService
    {
        private static UnitOfWork<MarketplaceDbContext>? unitOfWork;

        public UnitOfWork<MarketplaceDbContext> GetUnitOfWork()
        {
            if (unitOfWork == null)
            {
                unitOfWork = new UnitOfWork<MarketplaceDbContext>();
            }
            return unitOfWork;
        }
    }
}