using MarketplaceDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceDAL.Data
{
    public class MarketplaceDbContext: IdentityUserContext<IdentityUser>
    {
        public MarketplaceDbContext()
            : base()
        {
            //Database.EnsureCreated();
        }
        public DbSet<UserInfo> UsersInfo { get; set; }
        public DbSet<AdvType> AdvTypes { get; set; }
        public DbSet<Advertisement> Advertisements { get; set;}
        public DbSet<PurchasedAdvertisement> PurchasedAdvertisements { get; set; }
        public object Configuration { get; internal set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MarketplaceDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PurchasedAdvertisement>()
                .HasOne(p => p.PurchasedByUser)
                .WithMany(a => a.PurchasedAdvertisements)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AdvType>()
                .HasData(
                new AdvType { Id = 1, AdvTypeName = "advtype1" },
                new AdvType { Id = 2, AdvTypeName = "advtype2" },
                new AdvType { Id = 3, AdvTypeName = "advtype3" },
                new AdvType { Id = 4, AdvTypeName = "advtype4" },
                new AdvType { Id = 5, AdvTypeName = "advtype5" }
                );

            modelBuilder.Entity<UserInfo>()
                .HasData(
                new UserInfo
                { 
                    Id = 1, 
                    FirstName = "fname1", 
                    LastName = "lname1", 
                    BirthDate = new DateTime(),
                    Email = "email1@gmail.com",
                    DeliveryAdress = "dadres1",
                    City = "city1",
                    PhoneNumber = 232423423,
                    UserName = "username1"
                },
                new UserInfo
                {
                    Id = 2,
                    FirstName = "fname2",
                    LastName = "lname2",
                    BirthDate = new DateTime(),
                    Email = "email2@gmail.com",
                    DeliveryAdress = "dadres2",
                    City = "city2",
                    PhoneNumber = 232423423,
                    UserName = "username2"
                },
                new UserInfo
                {
                    Id = 3,
                    FirstName = "fname3",
                    LastName = "lname3",
                    BirthDate = new DateTime(),
                    Email = "email3@gmail.com",
                    DeliveryAdress = "dadres1",
                    City = "city1",
                    PhoneNumber = 332423423,
                    UserName = "username3"
                });

            modelBuilder.Entity<Advertisement>()
                .HasData(
                new Advertisement 
                { 
                    Id = 1, 
                    AdvName = "Test1", 
                    Description = "desc1", 
                    IsActive = true, 
                    Price = 1123.23M, 
                    SellerId = 1, 
                    YearOfManufacture = 2000
                },
                new Advertisement
                {
                    Id = 2,
                    AdvName = "Test2",
                    Description = "desc2",
                    IsActive = false,
                    isPurchased = true,
                    Price = 11423.23M,
                    SellerId = 2,
                    YearOfManufacture = 1900
                },
                new Advertisement
                {
                    Id = 3,
                    AdvName = "Test3",
                    Description = "desc3",
                    IsActive = true,
                    Price = 63123.23M,
                    SellerId = 3,
                    YearOfManufacture = 70650
                });

            modelBuilder.Entity<Advertisement>()
                .HasMany(at => at.AdvTypes)
                .WithMany(a => a.Advertisements)
                .UsingEntity(u => u.HasData(
                    new { AdvertisementsId = 1, AdvTypesId = 1 },
                    new { AdvertisementsId = 1, AdvTypesId = 3 },
                    new { AdvertisementsId = 1, AdvTypesId = 4 },
                    new { AdvertisementsId = 1, AdvTypesId = 5 },
                    new { AdvertisementsId = 2, AdvTypesId = 2 },
                    new { AdvertisementsId = 2, AdvTypesId = 1 },
                    new { AdvertisementsId = 3, AdvTypesId = 4 }
                ));

            modelBuilder.Entity<PurchasedAdvertisement>()
                .HasData(
                    new PurchasedAdvertisement { Id = 1, PurchasedByUserId = 2, AdvertisementId = 1 },
                    new PurchasedAdvertisement { Id = 2, PurchasedByUserId = 3, AdvertisementId = 2 },
                    new PurchasedAdvertisement { Id = 3, PurchasedByUserId = 1, AdvertisementId = 3 }
                );

            modelBuilder.Entity<AdvType>().HasIndex(u => u.AdvTypeName).IsUnique();
        }
    }
}
