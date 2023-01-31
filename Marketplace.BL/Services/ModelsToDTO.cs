using MarketplaceBL.ModelsDTO;
using MarketplaceDAL.Contracts;
using MarketplaceDAL.Models;
using MarketplaceDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBL.Services
{
    public static class ModelsToDTO
    {
        public static AdvertisementDTO AdvertisementToDTO(Advertisement entity)
        {
            return new AdvertisementDTO
            {
                Id = entity.Id,
                AdvName = entity.AdvName,
                IsActive = entity.IsActive,
                Price = entity.Price,
                isPurchased = entity.isPurchased,
                SellerId = entity.SellerId,
                Description = entity.Description,
                YearOfManufacture = entity.YearOfManufacture
            };
        }
        public static Advertisement AdvertisementPatchToDbModel(AdvertisementPATCH entity, Advertisement dbEntity)
        {
            if (entity.AdvName != null)
                dbEntity.AdvName = entity.AdvName;
            if (entity.IsActive != null)
                dbEntity.IsActive = (bool)entity.IsActive;
            if (entity.Price != null)
                dbEntity.Price = (decimal)entity.Price;
            if (entity.isPurchased != null)
                dbEntity.isPurchased = (bool)entity.isPurchased;
            if (entity.SellerId != null)
                dbEntity.SellerId = (int)entity.SellerId;
            if (entity.Description != null)
                dbEntity.Description = entity.Description;
            if (entity.YearOfManufacture != null)
                dbEntity.YearOfManufacture = entity.YearOfManufacture;

            return dbEntity;
        }
        public static AdvTypeDTO? AdvTypeToDTO(AdvType entity)
        {
            if (entity == null)
                return null;

            return new AdvTypeDTO
            {
                Id = entity.Id,
                AdvTypeName = entity.AdvTypeName
            };
        }
        public static PurchasedAdvertisementDTO PurchasedAdvertisementToDTO(PurchasedAdvertisement entity)
        {
            return new PurchasedAdvertisementDTO
            {
                Id = entity.Id,
                AdvertisementId = entity.AdvertisementId,
                PurchasedByUserId = entity.PurchasedByUserId,
                PurchaseDate = entity.PurchaseDate
            };
        }
        public static UserInfo UserDTOtoDbModel(UserDTO entity)
        {
            return new UserInfo
            {
                UserName = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DeliveryAdress = entity.DeliveryAdress,
                City = entity.City,
                PhoneNumber = entity.PhoneNumber,
                CreatedDate = entity.CreatedDate,
                BirthDate = entity.BirthDate,
                Email = entity.Email
            };
        }
        public static UserDTO UserInfoToDTO(UserInfo entity)
        {
            return new UserDTO
            {
                UserName = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DeliveryAdress = entity.DeliveryAdress,
                City = entity.City,
                PhoneNumber = entity.PhoneNumber,
                CreatedDate = entity.CreatedDate,
                BirthDate = entity.BirthDate,
                Email = entity.Email
            };
        }
    }
}
